﻿using FinanceWize.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceWize.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LiteDatabase _database;
        private readonly string collectionName = "transactions";
        public TransactionRepository(LiteDatabase database) {
            _database = database;
        }

        public List<Transaction> GetAll() {
            return _database.GetCollection<Transaction>(collectionName).Query().OrderByDescending(x=>x.Date).ToList();
        
        }

        public void Add(Transaction transaction) {
            var collection = _database.GetCollection<Transaction>(collectionName);
            collection.Insert(transaction);
            collection.EnsureIndex(x=>x.Date);

        }
        public void Update(Transaction transaction) {
            var collection = _database.GetCollection<Transaction>(collectionName);
            collection.Update(transaction);
        }

        public void Delete(Transaction transaction) {
            var collection = _database.GetCollection<Transaction>(collectionName);
            collection.Delete(transaction.Id);
        }
    }
}
