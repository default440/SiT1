#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <stdio.h>
#include <string.h>
#include <winsock2.h>
#include <windows.h>
#include <locale.h>

#define PORT 3001
#define SERVERADDR "127.0.0.1"

int main(int argc, char* argv[])
{
	setlocale(LC_ALL, "Russian");

	char buff[1024];
	
	WSADATA wd;
	if (WSAStartup(0x202, (WSADATA*)&wd))
	{
		printf("WSAStart error %d\n", WSAGetLastError());
		getchar();
		return -1;
	}

	SOCKET my_sock;
	my_sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (my_sock < 0)
	{
		printf("Socket() error %d\n", WSAGetLastError());
		getchar();
		return -1;
	}

	sockaddr_in dest_addr;
	dest_addr.sin_family = AF_INET;
	dest_addr.sin_port = htons(PORT);

	if (inet_addr(SERVERADDR) != INADDR_NONE)
		dest_addr.sin_addr.s_addr = inet_addr(SERVERADDR);
	else 
	{
		printf("Invalid address %s\n", SERVERADDR);
		closesocket(my_sock);
		WSACleanup();
		getchar();
		return -1;
	}

	if (connect(my_sock, (sockaddr*)&dest_addr, sizeof(dest_addr)))
	{
		printf("Connect error %d\n", WSAGetLastError());
		getchar();
		return -1;
	}

	char HELLO[] = "hello, world (C)";
	send(my_sock, HELLO, sizeof(HELLO), 0);

	int size = recv(my_sock, &buff[0], sizeof(buff) - 1, 0);
	printf("%s", buff);

	closesocket(my_sock);
	WSACleanup();

	getchar();

	return 0;
}