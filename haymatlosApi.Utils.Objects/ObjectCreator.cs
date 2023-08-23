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
        _user.IsIndexed = false;
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
        _post.Title = post.Title;
        _post.IsIndexed = false;

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
    public T createCommentObj(Guid postId, Comment comment, Guid? parentComment)
    {
        _comment.PkeyUuidComment = Guid.NewGuid();
        _comment.FkeyUuidPost = postId;
        _comment.Description = comment.Description;
        _comment.IsIndexed = false;
        _comment.ParentComment = parentComment;

        return _comment;
    }

    /* public T CreateObject()
     {
         return _createdObject;
     }*/
}
