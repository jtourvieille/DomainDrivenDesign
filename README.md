# Domain Driven Design

![Build CI Status](https://jto.visualstudio.com/_apis/public/build/definitions/ead6e1b7-db14-4396-8115-d903ce93329e/13/badge)

This is a set of libraries helping in the implementation of DDD.
We often need the same classes/interfaces when dealing with DDD: **ValueObject**, **Entity**, **AggregateRoot** ... are always quoted.

## Essentials.Domain

![Build Publish Status](https://jto.visualstudio.com/_apis/public/build/definitions/ead6e1b7-db14-4396-8115-d903ce93329e/12/badge)

### Quick Start

#### Defining your Entities

```
private class MyLongEntity : Entity<long>
{
	public MyLongEntity(long id)
	{
		this.Id = id;
	}
}

private class MyGuidEntity : Entity<Guid>
{
	public MyGuidEntity(Guid id)
	{
		this.Id = id;
	}
}
```
		
#### Defining your Value Objects

```
private class MyValueObject : ValueObject<MyValueObject>
{
    // Here are all properties of the Value Object
    public string MyStringProperty { get; set; }
    public int MyIntProperty { get; set; }
    public double MyDoubleProperty { get; set; }
	
    protected override IEnumerable<object> GetEqualityComponents()
    {
    	// Here are all properties taking part of the equality comparison
    	yield return MyIntProperty;
    	yield return MyStringProperty;
    }
}
```
			
#### Defining your Aggregate Root

This is as simple as
```
private class MyAggregate : AggregateRoot<Guid>
{
}
```

## Essentials.Infrastructure

![Build Publish Status](https://jto.visualstudio.com/_apis/public/build/definitions/ead6e1b7-db14-4396-8115-d903ce93329e/14/badge)

### Quick Start

This includes a **unit of work** implementation. Just use it like this

```
using (var transaction = _unitOfWork.BeginTransaction())
{
    var dbCommand = transaction.Connection.CreateCommand();
    dbCommand.CommandText = "SQL goes here";
    var result = dbCommand.ExecuteScalar(); //Or ExecuteReader ...
}
```

## Essentials.Infrastructure.Dapper

![Build Publish Status](https://jto.visualstudio.com/_apis/public/build/definitions/ead6e1b7-db14-4396-8115-d903ce93329e/15/badge)

This library implements a Dapper solution to the **Repository** pattern. Note that the repository applies to an AggregateRoot. The following example uses
both **async** repository and **Unit of Work** pattern. Note that the same repository exists in **sync**.

```
public class ProductAsyncRepository : DapperAsyncRepository<Product>
{
	public ProductAsyncRepository(IUnitOfWork unitOfWork)
		: base(unitOfWork)
	{
	}

	protected override ParametrizedSqlQuery SaveCommand(Product aggregateRoot)
	{
		return new ParametrizedSqlQuery(
			"INSERT INTO Product(id, Name, Description) VALUES (@Id, @Name, @Description)",
			new { aggregateRoot.Id, aggregateRoot.Name, aggregateRoot.Description });
	}

	protected override ParametrizedSqlQuery UpdateCommand(Product aggregateRoot)
	{
		return new ParametrizedSqlQuery(
			"UPDATE Product SET Name = @Name, Description = @Description WHERE Id = @Id ",
			new { aggregateRoot.Id, aggregateRoot.Name, aggregateRoot.Description });
	}
}
```

## Essentials.Application

![Build Publish Status](https://jto.visualstudio.com/DomainDrivenDesign/_apis/build/status/DomainDrivenDesign-Application-Publish)

This library holds a provider to store a map from domain event type to an enumeration of associated event type handlers.
You then have to instanciante them and call the **HandleAsync** method.

You First have to register all handlers. To do this, simply call the **Init** method of the **IDomainEventHandlersProvider** implementation.
You can do this operation once for all (generally at application startup).

This sample shows how to do it with Unity container, into the Global.asax file.

```
protected void Application_Start()
{
	UnityConfig.Container.Resolve<IDomainEventHandlersProvider>().Init(typeof(WhateverEventHandler).Assembly);
}
```

Every EventHandlers of this assembly will then be registered.

Finally, at runtime, you can create an instance of those types to call the **HandleAsync** method.

var handlersTypes = this.domainEventHandlersProvider.GetHandlersTypes(aggregateRootDomainEvent.GetType());

```
WhateverEvent whateverEvent = new WhateverEvent();
var handlersTypes = this.domainEventHandlersProvider.GetHandlersTypes(whateverEvent.GetType());

if (handlersTypes != null)
{
	foreach (var handlerType in handlersTypes)
	{
		dynamic handler = Activator.CreateInstance(handlerType);
		await handler.HandleAsync((dynamic) whateverEvent);
	}
}
```