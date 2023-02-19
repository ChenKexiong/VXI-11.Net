# VXI-11.Net
VXI-11.NET is a VXI-11 communication software for classroom training.

## Overview
- VXI-11 server and VXI-11 client by C#.
- It implements all message type of VXI-11.
- To focus on the main architecture of VXI-11, error handling is mostly omitted.
- Supports some functions of VISA and TMCTL libraries as interactive operation.
 
## Requirement
- Windows 10 and Linux.
- .Net 6 runtime.
- Knowledge of interactive operation on console.

## Usage
- Start the server on console window.
- Start the client of console operations
  - Enter the destination IP address on the client side.
  - Enter the command on the client side.
- On the server side.
  - Receivee messsage will be displayed.
  - Enter the response message.
- On the cliend side.
  - Receiveed messsage will be displayed.
- You can see TCP stream by using Wireshark.

# Reference
- [TCP/IP Instrument Protocol Specification VXI-11 Revision 1.0](https://www.vxibus.org/files/VXI_Specs/VXI-11.zip)
- [VPP-4.3.6:VISA Implementation Specification for .NET](https://www.ivifoundation.org/docs/vpp436_2016-06-07.pdf)
- [TMCTL](https://tmi.yokogawa.com/library/documents-downloads/software/tmctl/)

# Author
Twitter:@mitakalab

# License
GPL-2