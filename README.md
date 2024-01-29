## Assignments

### Task 1
> Add functionality to add/remove products to categories.
> Implement methods `PUT /products/{productId}/category/{categoryId}` and `DELETE /products/{productId}/category/{categoryId}`

Although this can be achieved by directly adding/deleting rows in the ProductCategory bridge table, which is simple enough for any dev one way or the other, I actually have a different POV.
From a business prespective, a category has products belonging to it, this relation can be easily represented in the C# code using navigation props, but a bridge table is needed to model this relation in the database.
This results in the existince of a entity like ProductCategory which can be thought of as a purely db conceren, that's why I don't like it and my approach was more focused on represneting this relation in the Domain models and eliminating the need for ProductCategory entity model, leaving it to the ORM to handle the database part for us (the joins/cascading action if needed).
As you know the main reason we use ORMs is to help us thinking in more of OOP fashion rather thinking about db tables.

I have configured NHibernate to use the existing bridge table without having to actually specifiy/manipulate a bridge table manually, made the PK of the bridge table auto generated
This will also have a better impact on the performance since the DB is better at doing joins instead of having to fetch the entities sepreatly and marry them in memory.

The only challenge with the approach is hanlding includes (aka eager loading related entities), which can easily be done by passing a predefiend set dictating what entities to include to the repo without exposing the service layer to db concerns.

### Task 2
> Add functionality to change products availability in different stores.
> Implement methods `POST /products/{productId}/book` and `POST /store/{storeId}/restock`

This is slightly different from the above, since a product availablity is a bussiness concept,

### Task 3
> Find and fix the memory leak issue.
> Improve the overall code quality in `GET /products/*` methods.

* Found the N+1 problem in the products controller when we fetch all products or products by category, for N number of products, we make N number of db trips to fetch their availablity, which is fixed as explained in Task 1
* Marked the SessionFactory DI life time as Singlton since we gonna be needing only 1 factory
* Ensure that Nhibernate.Session is being disposed (there are plenty of ways to do it, just ended up doing something quick and hacky :D)


## I have tried to show:
* Separation of concerns and modularity of design by splitting the solution into multiple projects, following a domain-centric approach.
* Staying up to date with the latest releases of the language and its features(pumped up the api to latest .NET version, along with the used libs), as shown by the excessive usage of the primary constructor :D :P.
* Basic understanding of Docker displayed through the changes made to be able to build a multi-project solution.
* Utilization of deferred execution and polymorphism to reduce code duplicate, showen with the CategoryRepo ProductCategoryQuery.
* The ability to rapidly get up and running with a framework that hasn't been worked with (at least in ages :D), talking about Nhibernate here.


## Feedback about the task itself (may some of these were intentional as part of the test :D :P)
* My Main concern was the reliance on NHibernate, it's rarely used these days compared to EF which has a better docs, bigger community, constant support and new relases, and even a better utilization of the concept of conventions over configuration
* The "Default" connection string was missing from appsettings.json, meaning the api will work only if the env var was passed from docker-compose
* The Entity mapping of Store was wrong, it mapped to the product table, https://gitlab.tsolutions.co/external/practice-exercises/turnit-generic-store/-/blob/master/Turnit.GenericStore.Api/Entities/Store.cs#L18
* Changed the LauchProfile "API" to display the swagger on start to ease the testing
* The addition of a db docker image is really helpful and I like it, but I am not sure about the value added by docker compose and dockerizing the API itself, in most cases the candidate will be developing/debugging the API, meaning that they will probably be using their IDE



## My ToDos:
* Edit the sql script to make the ProductCategory table's PK auto generated gen_random_uuid ()
* Better encapsulation
* Add different models for Persistence and Domain, and introduce more encapsulation
* Rename Turnit.Persistence to TurnitStore.Persistence
* Better discoverability through swagger and unifying the response type(perhaps ActionResult<T> for specficity)

