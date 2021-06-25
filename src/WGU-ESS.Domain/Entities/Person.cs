using System;

namespace WGU_ESS.Domain.Entities
{
  public abstract class Person
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public bool IsHidden { get; set; }
  }
}