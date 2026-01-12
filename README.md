# Unity 기반 멀티플레이어 게임 서버

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)

비트컴퓨터 Windows Developer 과정의 자율프로젝트로, C#과 System.Net.Sockets를 활용해 프레임워크 없이 서버-클라이언트 실시간 통신을 구축했습니다. TCP/UDP 하이브리드 네트워크와 MySQL DB 연동을 직접 구현한 네트워크 프로그래밍 프로젝트입니다.

## 🚀 기술 스택 (Tech Stack)

- **언어**: C# 91.1%, ASP.NET 8.9%
- **네트워크**: TCP/UDP 소켓 프로그래밍 (System.Net.Sockets), 커스텀 패킷 프로토콜
- **데이터베이스**: MySQL, SQL 쿼리 처리
- **엔진/툴**: Unity, Visual Studio
- **기타**: 비동기 I/O, 틱 기반 서버 루프 (30틱/초)

## 🔧 구현 세부 (Implementation Details)

### 네트워크 아키텍처
- **TCP 소켓**: TcpListener로 서버 포트 개방, TcpClient로 클라이언트 연결. 신뢰성이 필요한 로그인/회원가입 데이터를 처리.
- **UDP 소켓**: UdpClient로 실시간 게임 로직(플레이어 위치 동기화) 전송.
- **패킷 설계**: Packet 클래스로 헤더(길이/ID) + 바디(데이터)를 구조화. 직렬화/역직렬화로 바이트 변환.
- **DB 연동**: MySQL 쿼리로 사용자 인증 및 데이터 저장.

### 주요 코드 구조
- **Server.cs**: 소켓 리스너 및 클라이언트 관리
- **Client.cs**: TCP/UDP 연결 핸들링
- **ServerSend.cs**: 패킷 전송 로직
- **DB.cs**: SQL 쿼리 처리
- **ServerHandler.cs**: 패킷 핸들러 (로그인/회원가입 로직)

### 도전
- 프레임워크 없이 소켓과 패킷을 직접 다루며 멀티클라이언트 동시성을 관리.
- TCP/UDP 특성을 이해하고 하이브리드 구조 설계 (신뢰성 vs. 실시간성).

