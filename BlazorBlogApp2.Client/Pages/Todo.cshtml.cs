using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorBlogApp2.Shared;
using Blazored.Storage;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorBlogApp2.Client.Pages
{
    public class TodoModel : BlazorComponent
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        [Inject]
        private AppState _appState { get; set; }

        [Inject]
        private IUriHelper _uriHelper { get; set; }

        [Inject]
        private ILocalStorage _localStorage { get; set; }

        protected string itemName;

        protected IList<ToDoItem> todos = new List<ToDoItem>();

        protected override async Task OnInitAsync()
        {
            if (_appState.IsLoggedIn)
            {
                await Refresh();
            }
        }

        private async Task Refresh()
        {
            todos = await _httpClient.GetJsonAsync<List<ToDoItem>>("api/todo");
        }

        public async Task AddToDo()
        {
            if (!string.IsNullOrEmpty(itemName))
            {
                await _httpClient.PostJsonAsync("api/todo", new ToDoItem
                {
                    Item = itemName
                });

                itemName = "";
            }

            await Refresh();
        }

        public async Task DeleteAllToDos()
        {
            await _httpClient.DeleteAsync("api/todo/all");

            await Refresh();
        }

        public void RefreshAfterDelete(ToDoItem deletedItem)
        {
            todos.Remove(deletedItem);

            StateHasChanged();
        }
    }
}