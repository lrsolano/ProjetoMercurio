using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class ItemConverter : IParser<ItemV, Item>, IParser<Item, ItemV>
    {
        public ItemV Parser(Item origin)
        {
            if (origin == null) return null;
            ItemV item = new ItemV
            {
                Id = origin.Id,
                Nome = origin.Nome,
                Quantidade = origin.Quantidade
            };
            return item;
        }
        public Item Parser(ItemV origin)
        {
            if (origin == null) return null;
            Item item = null;
            if (origin.Id != 0)
            {
                item = Item.FindById(origin.Id);
            }
            else
            {
                item = new Item(origin.Nome);
            }
            item.AddQuantidade(origin.Quantidade);
            return item;
        }

        public List<ItemV> Parser(List<Item> origin)
        {
            if (origin.Count == 0) return null;
            List<ItemV> items = new List<ItemV>();
            foreach (Item u in origin)
            {
                items.Add(Parser(u));
            }
            return items;
        }

        

        public List<Item> Parser(List<ItemV> origin)
        {
            if (origin.Count == 0) return null;
            List<Item> items = new List<Item>();
            foreach (ItemV u in origin)
            {
                items.Add(Parser(u));
            }
            return items;
        }
    }
}
