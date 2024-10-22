using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Helper
{
  public class FileRequest
  {
    public byte[]? Bytes { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
  }
}