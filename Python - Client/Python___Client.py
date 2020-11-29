import socket 

sock = socket.socket()

sock.connect(('localhost', 3002))

sock.send("hello, world (Python)".encode())

data = sock.recv(1024)
print (data.decode())

sock.close()