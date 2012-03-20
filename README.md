##MakeDataTable.From

This is simply a couple static functions for building/stubbing a `DataTable` object from objects built in code.

###Background

I have worked with quite a bit of legacy code that is so tightly coupled with data access that it made testing without a full-fledged SQL database very difficult. Having such a DB, made unit tests slow (building DB) and fragile (clearing out data from prior tests). I put together this system for stubbing out a data layer that returns `DataTable` objects (this legacy code did) without having to manually build all the `DataTable` objects by hand.

This system has some of its own fragility, but I think it outweighs the disadvantages of using SQL database or building a `DataTable` by hand. In this case, `MakeDataTable.From` is fairly trusting of the objects you send it to turn into a `DataTable` and will likely explode spectacularly when you feed it something unusual.