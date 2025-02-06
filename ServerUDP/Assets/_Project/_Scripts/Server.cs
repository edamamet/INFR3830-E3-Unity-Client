using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
namespace Networking.Core {
    public class Server : MonoBehaviour {
        static byte[] buffer = new byte[1024];
        static IPEndPoint localEp;
        static Socket serverSocket;
        static EndPoint client;
        [SerializeField] bool receiving;
        [SerializeField] float interval;
        [SerializeField] Transform obj;
        float currentTime;

        static void Init() {
            var ip = IPAddress.Parse("127.0.0.1");
            var host = Dns.GetHostEntry(ip);
            Debug.Log($"{host.HostName} @ {ip}");
            localEp = new(ip, 6969);
            serverSocket = new(
                ip.AddressFamily,
                SocketType.Dgram,
                ProtocolType.Udp);
            var clientEp = new IPEndPoint(IPAddress.Any, 0);
            client = clientEp;
            serverSocket.Bind(localEp);
            Debug.Log("Waiting for data...");
        }

        void Receive() {
            var bytesReceived = serverSocket.ReceiveFrom(buffer, ref client);
            var message = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
            Debug.Log("Received: " + message);
            var vector = message.Split(',').Select(float.Parse).ToArray();
            obj.position = new Vector3(vector[0], vector[1], vector[2]);
        }

        void Start() {
            Init();
        }
        void Update() {
            if (!receiving || UpdateTime() <= interval) return;
            currentTime = 0;
            Receive();
        }

        float UpdateTime() {
            currentTime += Time.deltaTime;
            return currentTime;
        }
    }
}