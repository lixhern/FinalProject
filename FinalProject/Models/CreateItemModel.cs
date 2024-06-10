using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Identity.Client;
using System.Collections;

namespace FinalProject.Models
{
    public class CreateItemModel
    {
        public List<Item> Items { get; set; }
        public Collection Collection { get; set; }


        public CreateItemModel(List<Item> Items, Collection Collection) 
        {
            this.Items = Items;
            this.Collection = Collection;
        }
    }
}
