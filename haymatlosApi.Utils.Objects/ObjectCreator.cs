using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.Utils;
using Microsoft.Extensions.Hosting;
using System;

public class ObjectFactoryUser<T> where T : User, new()
{
    private T _user;

    public ObjectFactoryUser()
    {
        _user = new T();
    }

    //public ObjectFactory<T> WithUserSetup(string nickname, string password, string passwordSalt)
    public T createUserObj(string nickname, string password, string passwordSalt)
    {
        _user.Nickname = nickname; 
        _user.Uuid = Guid.NewGuid();
        _user.Salt = passwordSalt;
        _user.RegDate = DateTime.UtcNow;
        _user.Role = "user";
        _user.Password = PasswordHashing.ComputeHash(password, passwordSalt);

        return _user;
    }
}

public class ObjectFactoryPost<T> where T : Post, new()
{
    private T _post;

    public ObjectFactoryPost()
    {
        _post = new T();
    }
    public T createPostObj(Guid userId, Post post)
    {
        _post.PkeyUuidPost = Guid.NewGuid();
        _post.FkeyUuidUser = userId;
        _post.Like = 0;
        _post.Dislike = 0;
        _post.RegDate = DateTime.UtcNow;
        _post.Title = post.Title; 

        return _post;
    }
}

public class ObjectFactoryComment<T> where T : Comment, new()
{
    private T _comment;

    public ObjectFactoryComment()
    {
        _comment = new T();
    }
    public T createCommentObj(Post post, Comment comment, Guid? parentComment)
    {
        _comment.PkeyUuidComment = Guid.NewGuid();
        _comment.FkeyUuidPost = post.PkeyUuidPost;
        _comment.RegDate = DateTime.UtcNow;
        _comment.Description = comment.Description;
        _comment.Like = 0;
        _comment.Dislike = 0;
        _comment.FkeyUuidUser = post.FkeyUuidUser;
        _comment.ParentComment = parentComment;

        return _comment;
    }

    /* public T CreateObject()
     {
         return _createdObject;
     }*/
}

/*
 {
	"post": {
		"pkeyUuidPost": "9ec06c37-e22a-4ca1-8b04-554e3b60af15",
		"title": "string",
		"fkeyUuidUser": "a89d56f1-d405-453e-90be-cc02e2c896ce"

	},
	"comment": {
		"description": "dfghj"
	}
}
 */