using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorBlogApp2.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;

namespace BlazorBlogApp2.Client.Pages
{
    public class TodoViewModel : BlazorComponent
    {
        [Parameter] protected ToDoItem TodoItem { get; set; }

        [Parameter] Action<ToDoItem> Callback { get; set; }

        protected string UpdateItemName;

        [Inject] private HttpClient _httpClient { get; set; }

        public async Task UpdateToDo()
        {
            if (!string.IsNullOrEmpty(UpdateItemName))
            {
                TodoItem.Item = UpdateItemName;

                await _httpClient.PutJsonAsync($"api/todo/{TodoItem.Id}", TodoItem);

                await JSRuntime.Current.InvokeAsync<bool>("todo.showControl", TodoItem.Id.ToString(), "", false);
            }
        }

        public async Task DeleteToDo()
        {
            await _httpClient.DeleteAsync($"api/todo/{TodoItem.Id}");

            Callback.Invoke(TodoItem);
        }

        public async Task EditToDo()
        {
            await JSRuntime.Current.InvokeAsync<bool>("todo.showControl", TodoItem.Id.ToString(), TodoItem.Item, true);
        }

        public async Task CancelToDo()
        {
            await JSRuntime.Current.InvokeAsync<bool>("todo.showControl", TodoItem.Id.ToString(), "", false);
        }
    }
}