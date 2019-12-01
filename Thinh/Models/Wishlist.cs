using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
	public static class SessionExtensions
	{
		public static void SetObjectAsJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetObjectFromJson<T>(this ISession session, string key)
		{
			var value = session.GetString(key);

			return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		}
	}
	public static class Wishlist
	{
		private const string CartSessionKey = "Wishlist";
		public static List<Product> GetList(this ISession session)
		{
			return session.GetObjectFromJson<List<Product>>(CartSessionKey) ?? new List<Product>();
		}

		public static void SaveList(this ISession session, List<Product> cart)
		{
			session.SetObjectAsJson(CartSessionKey, cart);
		}
	}
}
