﻿using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoProdutoAdicionadoEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public PedidoProdutoAdicionadoEvent(Guid clienteId, Guid pedidoId, Guid produtoId, int quantidade)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
    }
}