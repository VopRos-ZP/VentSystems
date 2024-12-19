using System.Collections.Generic;
using VentSystems.Model;

namespace VentSystems.Utils
{
    public static class Db
    {
        public static readonly VentsEntities Entities = new VentsEntities();

        public static List<string> TableNames = new List<string>
        {
            "Продукты",
            "Пользователи",
            "Роли",
            "Страны",
            "Склады",
            "Поставщики",
        };
    }
}