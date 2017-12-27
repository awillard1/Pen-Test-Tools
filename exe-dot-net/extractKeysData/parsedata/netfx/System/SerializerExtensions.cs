#region BSD License
/* 
Copyright (c) 2011, NETFx
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list 
  of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this 
  list of conditions and the following disclaimer in the documentation and/or other 
  materials provided with the distribution.

* Neither the name of Clarius Consulting nor the names of its contributors may be 
  used to endorse or promote products derived from this software without specific 
  prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH 
DAMAGE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/// <summary>
/// Provides simpler serialization and deserialization extension methods 
/// for <see cref="ISerializer"/> over a byte array.
/// </summary>
static partial class SerializationExtensions
{
	/// <summary>
	/// Deserializes an object graph of type <typeparamref name="T"/> from 
	/// the given byte array.
	/// </summary>
	/// <typeparam name="T">The type of object graph to deserialize.</typeparam>
	/// <param name="serializer">The serializer to use.</param>
	/// <param name="serialized">The serialized byte array.</param>
	public static T Deserialize<T>(this ISerializer serializer, byte[] serialized)
	{
		Guard.NotNull(() => serializer, serializer);
		Guard.NotNull(() => serialized, serialized);

		if (serialized.Length == 0)
			return default(T);

		using (var stream = new MemoryStream(serialized))
		{
			return serializer.Deserialize<T>(stream);
		}
	}

	/// <summary>
	/// Serializes the given object graph as a byte array.
	/// </summary>
	/// <typeparam name="T">The type of object graph to serialize, inferred by the
	/// compiler from the passed-in <paramref name="graph"/>.</typeparam>
	/// <param name="serializer">The serializer to use.</param>
	/// <param name="graph">The object graph to serialize.</param>
	/// <returns>The byte array containing the serialized object graph.</returns>
	public static byte[] Serialize<T>(this ISerializer serializer, T graph)
	{
		Guard.NotNull(() => serializer, serializer);
		Guard.NotNull(() => graph, graph);

		using (var stream = new MemoryStream())
		{
			serializer.Serialize<T>(stream, graph);

			return stream.ToArray();
		}
	}
}