##MakeDataTable.From

This is simply a couple static functions for building/stubbing a `DataTable` object from objects built in code.

###Examples

####Basic

    DataTable test = MakeDataTable.From(new { x = 1, y = (int?)null, z = "test" });

####Default row values (one method)

This isn't the only way to do this, but it does the job. Create a factory method that has all the defaults defined in default parameter values. In any row created, override any defaults in the method call with named parameters.

    DataTable someTable = MakeDataTable.From(
        new[] {
            CreateSomeRow(1),
            CreateSomeRow(2, parentId: 1, name: "Custom name"),
            CreateSomeRow(3, parentId: 1, isVisible: false),
            CreateSomeRow(4, parentId: 2),
            CreateSomeRow(5, parentId: 3, name: "A different name", isVisible: false),
        });
    private static dynamic CreateSomeRow(int id,
                                         int? parentId = null,
                                         string name = "Default name",
                                         bool isVisible = true) {
        return new {
            Id = id,
            ParentId = parentId,
            Name = name,
            IsVisible = isVisible
        };
    }

###Background

I have worked with quite a bit of legacy code that is so tightly coupled with data access that it made testing without a full-fledged SQL database very difficult. Having such a DB, made unit tests slow (building DB) and fragile (clearing out data from prior tests). I put together this system for stubbing out a data layer that returns `DataTable` objects (this legacy code did) without having to manually build all the `DataTable` objects by hand.

This system has some of its own fragility, but I think it outweighs the disadvantages of using SQL database or building a `DataTable` by hand. In this case, `MakeDataTable.From` is fairly trusting of the objects you send it to turn into a `DataTable` and will likely explode spectacularly when you feed it something unusual.