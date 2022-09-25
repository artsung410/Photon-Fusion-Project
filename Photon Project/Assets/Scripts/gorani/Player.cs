using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : NetworkBehaviour
{
    public static event Action<string> ChatSignal = delegate { };

    // MyByte가 바뀔때마다 함수를 호출하게 할수도있다.

    //[Networked(OnChanged = nameof(MyByteChanged))] 

    [Networked] public byte MyByte { get; set; }

    NetworkCharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
        }
    }

    public static void MyByteChanged(Changed<Player> changed)
    {
        if (changed.Behaviour)
        {
            changed.Behaviour.MyByteChanged2();
        }
    }

    void MyByteChanged2()
    {
        Debug.Log(MyByte);
        Debug.Log("경일게임아카데미");
    }

    private void Update()
    {
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.F))
        {
            MyByte = (byte)UnityEngine.Random.Range(0, 254);
        }

        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.Return))
        {
            if (PlayerHUD.Instance.chatBox.text != "")
            {
                RPC_SendMessage(PlayerHUD.Instance.chatBox.text);
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        if (info.Source == Runner.Simulation.LocalPlayer)
            message = $"You: {message}\n";
        else
            message = $"Other: {message}\n";
        
        ChatSignal(message);
    }
}
