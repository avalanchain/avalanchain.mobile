using System.Collections.Generic;

namespace avalanchain
{
	public class PostsViewModel
	{
		public List<Post> PostsList 
		{ 
			get 
			{
				return SampleData.Posts;
			}
		}
	}
}

