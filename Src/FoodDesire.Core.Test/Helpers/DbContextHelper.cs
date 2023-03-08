﻿using Microsoft.EntityFrameworkCore;

namespace FoodDesire.Core.Test.Helpers;
public static class DbContextHelper {
    public static void ConfigureDbContextOptions(string name, DbContextOptionsBuilder options) {
        //options.UseInMemoryDatabase(name);
        options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database=foodDesireDB;Integrated Security=True;");
    }
}
