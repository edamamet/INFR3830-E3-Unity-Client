using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
namespace Networking.Core {
    public class Client : MonoBehaviour {
        [SerializeField] Transform obj;
        static byte[] buffer = new byte[1024];
        static IPEndPoint remoteEp;
        static Socket clientSocket;

        static void Init() {
            try {
                var ip = IPAddress.Parse("127.0.0.1");
                remoteEp = new(ip, 6969);
                clientSocket = new(
                    ip.AddressFamily,
                    SocketType.Dgram,
                    ProtocolType.Udp);
            } catch (Exception e) {
                var error = e switch {
                    SocketException se => $"The server is not available. {se.Message}",
                    ArgumentNullException ane => $"An argument is null. {ane.Message}",
                    _ => e.Message,
                };
                Debug.LogError(error);
            }
        }

        void Start() {
            Init();
        }

        void Update() {
            Send();
        }

        void Send() {
            buffer = Encoding.UTF8.GetBytes(PositionToString(obj.position));
            clientSocket.SendTo(buffer, remoteEp);
            Debug.Log("Sent: " + PositionToString(obj.position));
        }

        static string PositionToString(Vector3 position) {
            return $"{position.x},{position.y},{position.z}";
        }
    }
}
