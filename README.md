CashMachine
===========

An ASP .NET MVC Simulator of a Cash Machine.
It resolves a Card depending on a Number and a Pin Code. Then it gives an Access to Balance and Cash Operations.
Operations are logged under "Operations" Table and Cards are kept under "Cards" Table in Database.
Cards are stored along with their Pin Codes which are hashed for a better Security. MD5 Algorithm with Salt and Pepper is used.
Project named CashMachine.Storage manages all that Data using Entity Framework. However all the implementation is incapsulated inside it.
Client can only operate Data Model via Data Provider described in CashMachine.Data Project. Data Provider is matched to it's implementation using Unity Framework.
Each Controller is inherited from ControllerBase, which provides basic Methods to navigate from one Page to another and stores a Data Provider.

The following Cards are available:
1111-1111-1111-1111    Pin: 1111
2222-2222-2222-2222    Pin: 2222 (it's blocked for Test Purposes)
1234-5678-1234-5678    Pin: 1234
1234-5678-9012-3456    Pin: 1234
