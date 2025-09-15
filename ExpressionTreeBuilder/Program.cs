// See https://aka.ms/new-console-template for more information


using BusinessEntities;
using Data.Connections;
using Data.DbContexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

List<User> Users = new List<User>{
    new User { Name = "nev", Email = "nev.com" },
    new User { Name = "ken", Email = "ken.com" }
};

//DataDbContext DbContext = new();

//ConstantExpression constantA = Expression.Constant("18", typeof(string));
//ConstantExpression constantB = Expression.Constant("18", typeof(string));
//ConstantExpression constantC = Expression.Constant(new User { Name="mad", Email="mad.com"}, typeof(User));

//ParameterExpression parameterA = Expression.Parameter(typeof(string), "Email");
//ParameterExpression parameterB = Expression.Parameter(typeof(string), "AnotherEmail");
//ParameterExpression parameterC = Expression.Parameter(typeof(User), "User");

////MemberExpression memberA = Expression.Property(parameterA, "Email");
////MemberExpression memberB = Expression.Property(parameterB, "AnotherEmail");

//BinaryExpression equalBody = Expression.Equal(constantA, constantB);
//UnaryExpression unary = Expression.IsTrue(equalBody);
////BinaryExpression body = Expression.IsTrue(parameterA);

////BinaryExpression finalExpression = Expression.AndAlso(body, body2);

//Expression<Func<User, bool>> equality = (user) => user.Email =="ken.com";

//Expression<Func<User, bool>> result = Expression.Lambda<Func<User, bool>>(equality, parameterC);

//Expression<Func<User, bool>> expressionA = result;
////Expression<Func<User, bool>> expressionB = exp => exp.Email == "ken.com";


//IQueryable<User> queryable = DbContext.User.Where(expressionA);

//var query = queryable.ToQueryString();



//ParameterExpression pe = Expression.Parameter(typeof(string), "Email");
//MemberExpression mexp = Expression.Property(pe, "Age");
//ConstantExpression constant = Expression.Constant(18, typeof(int));


//BinaryExpression body = Expression.Equal(mexp, constant);
//BinaryExpression body2 = Expression.Equal(mexp, constant);
//BinaryExpression finalExpression = Expression.AndAlso(body, body);
//Expression<Func<User, bool>> result = Expression.Lambda<Func<User, bool>>(finalExpression);

//result.Compile().DynamicInvoke();

//Expression<Func<User, bool>> expressionA = result;
//Expression<Func<User, bool>> expressionB = exp => exp.Email == email;




Console.WriteLine();
