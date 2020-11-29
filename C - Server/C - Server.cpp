#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <stdio.h>
#include <winsock2.h>
#include <windows.h>
#include <locale.h>

#define PORT 3001

int main(int argc, char* argv[])
{
	setlocale(LC_ALL, "Russian");

	char buff[1024];
	
	WSADATA wd;
	if (WSAStartup(0x0202, (WSADATA*)&wd))
	{
		printf("Error WSAStartup %d\n", WSAGetLastError());
		return -1;
	}

	SOCKET mysocket;
	if ((mysocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		printf("Error socket %d\n", WSAGetLastError());
		WSACleanup();
		return -1;
	}

	sockaddr_in local_addr;
	local_addr.sin_family = AF_INET;
	local_addr.sin_port = htons(PORT);
	local_addr.sin_addr.s_addr = INADDR_ANY;

	if (bind(mysocket, (sockaddr*)&local_addr, sizeof(local_addr)))
	{
		printf("Error bind %d\n", WSAGetLastError());
		closesocket(mysocket);
		WSACleanup();
		return -1;
	}

	// размер очереди – 0x100
	if (listen(mysocket, 0x100))
	{
		printf("Error listen %d\n", WSAGetLastError());
		closesocket(mysocket);
		WSACleanup();
		return -1;
	}

	SOCKET client_socket;
	sockaddr_in client_addr;
	int client_addr_size = sizeof(client_addr);

	client_socket = accept(mysocket, (sockaddr*)&client_addr, &client_addr_size);

	int size = recv(client_socket, &buff[0], sizeof(buff) - 1, 0);
	printf("%s", buff);

	send(client_socket, buff, sizeof(buff), 0);

	closesocket(client_socket);
	closesocket(mysocket);

	return 0;
}