import socket 
sock = socket.socket()
sock.bind(('', 3002))
sock.listen(1)

conn, addr = sock.accept()

data = conn.recv(1024)
print (data.decode())

conn.send(data)

conn.close()

