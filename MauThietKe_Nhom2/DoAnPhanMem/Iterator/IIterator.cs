using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DoAnPhanMem.Models;
using Newtonsoft.Json;
using PagedList;

namespace DoAnPhanMem.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool IsDone();
    }

    // Concrete Iterator
    public class AddressIterator : IIterator<AccountAddress>
    {
        private List<AccountAddress> _collection;
        private int _position = 0;

        public AddressIterator(List<AccountAddress> collection)
        {
            this._collection = collection;
        }

        public AccountAddress First()
        {
            _position = 0;
            return _collection[_position];
        }

        public AccountAddress Next()
        {
            _position++;
            return _collection[_position];
        }

        public bool IsDone()
        {
            return _position >= _collection.Count;
        }
    }
}
