/*
 * Script para instalação do banco de dados (SQL Server)
 */
CREATE DATABASE Vendas;

GO
USE Vendas;

GO
CREATE TABLE [dbo].[Cliente] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Nome] NVARCHAR (100) NOT NULL,
    [Cpf]  NVARCHAR (11) NOT NULL,
    CONSTRAINT [PK_dbo.Cliente] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TABLE [dbo].[Produto] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Descricao] NVARCHAR (100)  NOT NULL,
    [Valor]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_dbo.Produto] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TABLE [dbo].[PedidoVenda] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [DataEntrega] DATETIME        NOT NULL,
    [Numero]      INT             NOT NULL,
    [ValorTotal]  DECIMAL (18, 2) NOT NULL,
    [Cliente_Id]  INT             NOT NULL,
    CONSTRAINT [PK_dbo.PedidoVenda] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PedidoVenda_dbo.Cliente_Cliente_Id] FOREIGN KEY ([Cliente_Id]) REFERENCES [dbo].[Cliente] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_Cliente_Id]
    ON [dbo].[PedidoVenda]([Cliente_Id] ASC);

GO
CREATE TABLE [dbo].[ItemPedidoVenda] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Quantidade] FLOAT (53)      NOT NULL,
    [Valor]      DECIMAL (18, 2) NOT NULL,
    [ValorTotal] DECIMAL (18, 2) NOT NULL,
    [Pedido_Id]  INT             NOT NULL,
    [Produto_Id] INT             NOT NULL,
    CONSTRAINT [PK_dbo.ItemPedidoVenda] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ItemPedidoVenda_dbo.PedidoVenda_Pedido_Id] FOREIGN KEY ([Pedido_Id]) REFERENCES [dbo].[PedidoVenda] ([Id]),
    CONSTRAINT [FK_dbo.ItemPedidoVenda_dbo.Produto_Produto_Id] FOREIGN KEY ([Produto_Id]) REFERENCES [dbo].[Produto] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_Pedido_Id]
    ON [dbo].[ItemPedidoVenda]([Pedido_Id] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Produto_Id]
    ON [dbo].[ItemPedidoVenda]([Produto_Id] ASC);

GO

INSERT INTO Cliente (Nome, Cpf) VALUES ('CONSUMIDOR GENERICO', '');
INSERT INTO Cliente (Nome, Cpf) VALUES ('Denis Bittencourt', '98262100565');
INSERT INTO Cliente (Nome, Cpf) VALUES ('Diego Muniz', '61202277764');
INSERT INTO Cliente (Nome, Cpf) VALUES ('Dinorah Gabriel', '26361532410');
INSERT INTO Cliente (Nome, Cpf) VALUES ('Ronaldo Beraldo', '78179211800');

GO

INSERT INTO Produto (Descricao, Valor) VALUES ('Smart TV LG 42"', 2100);
INSERT INTO Produto (Descricao, Valor) VALUES ('Notebook Dell Vostro 3550', 2750);
INSERT INTO Produto (Descricao, Valor) VALUES ('ZenFone 2 16GB', 1250);
INSERT INTO Produto (Descricao, Valor) VALUES ('Livro .Net Core Resumão', 250);
INSERT INTO Produto (Descricao, Valor) VALUES ('Mouse Logitech sem fio', 50);
INSERT INTO Produto (Descricao, Valor) VALUES ('MousePad cor sólida', 2.5);
INSERT INTO Produto (Descricao, Valor) VALUES ('Caneca linguagens - branco ou preto', 12);
INSERT INTO Produto (Descricao, Valor) VALUES ('Teclado Microsoft Wired 800', 131);
INSERT INTO Produto (Descricao, Valor) VALUES ('Impressora HP Deskjet Ink Advantage', 199.99);