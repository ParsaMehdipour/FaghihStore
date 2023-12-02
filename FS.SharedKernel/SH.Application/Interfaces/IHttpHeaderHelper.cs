﻿namespace SH.Application.Interfaces;

public interface IHttpHeaderHelper
{
    public T Get<T>(string key);
    public string Get(string key);
    public bool ContainsKey(string key);
    public bool ContainsValue(string key, string value);
    public void Set(string key, string value);
    public void Set<T>(string key, T value);
    public void Delete(string key);
}
