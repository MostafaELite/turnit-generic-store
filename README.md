## Assignments
You are free to use any additional libraries, patterns, etc that you find fit (except replacing NHibernate). Database changes are not required.

### Task 1
> Add functionality to add/remove products to categories.
> Implement methods `PUT /products/{productId}/category/{categoryId}` and `DELETE /products/{productId}/category/{categoryId}`


### Task 2
> Add functionality to change products availability in different stores.
> Implement methods `POST /products/{productId}/book` and `POST /store/{storeId}/restock`

### Task 3
> Find and fix the memory leak issue.
> Improve the overall code quality in `GET /products/*` methods.


* Added the "Default" connection string to appsettings.json
* Changed the LauchProfile "API" to display the swagger on start



