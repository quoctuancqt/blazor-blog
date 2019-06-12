using Microsoft.AspNetCore.Blazor.Components;
using System.Collections.Generic;

namespace SimpleList.Component
{
    public class SimpleListModel : BlazorComponent
    {
        public List<string> ToDoLists = new List<string>();

        public string newItem;

        public void AddItem()
        {
            if (string.IsNullOrEmpty(newItem)) return;

            ToDoLists.Add(newItem);

            newItem = "";
        }
    }
}