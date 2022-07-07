﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar 
{
    public class DeleteNavTaskInit<Root,T> where T : class, new() where Root : class, new()
    {
        public List<T> Roots { get;   set; }
        internal SqlSugarProvider Context { get; set; }
        internal DeleteNavProvider<Root, Root> deleteNavProvider { get; set; }

        public DeleteNavTask<Root, TChild> Include<TChild>(Expression<Func<Root, TChild>> expression) where TChild : class, new()
        {
            this.Context = deleteNavProvider._Context;
            DeleteNavTask<Root, TChild> result = new DeleteNavTask<Root, TChild>();
            Func<DeleteNavTask<Root, TChild>> func = () => deleteNavProvider.ThenInclude(expression);
            result.PreFunc = func;
            result.Context = this.Context;
            return result;
        }
        public DeleteNavTask<Root, TChild> Include<TChild>(Expression<Func<Root, List<TChild>>> expression) where TChild : class, new()
        {
            this.Context = deleteNavProvider._Context;
            DeleteNavTask<Root, TChild> result = new DeleteNavTask<Root, TChild>();
            Func<DeleteNavTask<Root, TChild>> func = () => deleteNavProvider.ThenInclude(expression);
            result.PreFunc = func;
            result.Context = this.Context;
            return result;
        }
    }
    public class DeleteNavTask<Root, T> where T : class, new() where Root : class, new()
    {
        public SqlSugarProvider Context { get; set; }
        public Func<DeleteNavTask<Root, T>> PreFunc { get; set; }
        public DeleteNavTask<Root, TChild> ThenInclude<TChild>(Expression<Func<T, TChild>> expression) where TChild : class, new()
        {
            DeleteNavTask<Root, TChild> result = new DeleteNavTask<Root, TChild>();
            Func<DeleteNavTask<Root, TChild>> func = () => PreFunc().ThenInclude(expression);
            result.PreFunc = func;
            result.Context = this.Context;
            return result;
        }
        public DeleteNavTask<Root, TChild> ThenInclude<TChild>(Expression<Func<T, List<TChild>>> expression) where TChild : class, new()
        {
            DeleteNavTask<Root, TChild> result = new DeleteNavTask<Root, TChild>();
            Func<DeleteNavTask<Root, TChild>> func = () => PreFunc().ThenInclude(expression);
            result.PreFunc = func;
            result.Context = this.Context;
            return result;
        }
        public DeleteNavTask<Root, TChild> Include<TChild>(Expression<Func<Root, TChild>> expression) where TChild : class, new()
        {
            return AsNav().ThenInclude(expression);
        }
        public DeleteNavTask<Root, TChild> Include<TChild>(Expression<Func<Root, List<TChild>>> expression) where TChild : class, new()
        {
            return AsNav().ThenInclude(expression);
        }
        public bool ExecuteCommand()
        {
            var hasTran = this.Context.Ado.Transaction != null;
            if (hasTran)
            {
                PreFunc();
            }
            else
            {
                this.Context.Ado.UseTran(() =>
                {
                    PreFunc();
                }, ex => throw ex);
            }
            return true;
        }
        public async Task<bool> ExecuteCommandAsync()
        {
            await Task.Run(async () =>
            {
                ExecuteCommand();
                await Task.Delay(0);
            });
            return true;
        }

        private DeleteNavTask<Root, Root> AsNav()
        {
            DeleteNavTask<Root, Root> result = new DeleteNavTask<Root, Root>();
            Func<DeleteNavTask<Root, Root>> func = () => PreFunc().AsNav();
            result.PreFunc = func;
            result.Context = this.Context;
            return result;
        }
    }
}
