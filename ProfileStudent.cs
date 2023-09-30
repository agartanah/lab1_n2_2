using System;

namespace lab1_n2_2 {
  public class ProfileStudent {
    public string LastName { get; set; }
    public string FirstName { get; set; }
    private int schoolNumber;
    public int SchoolNumber {
      get {
        return schoolNumber;
      }
      set {
        if (value <= 5 && value >= 1) {
          schoolNumber = value;
        } else {
          throw new Exception("Нет такой школы !!!");
        }
      }
    }
    private int gradeMath;
    public int GradeMath { 
      get {
        return gradeMath;
      }
      set { 
        if (value >= 1 && value <= 5) {
          gradeMath = value;
        } else {
          gradeMath = 1;
        }
      }
    }
    private int gradeLit;
    public int GradeLit {
      get {
        return gradeLit;
      }
      set {
        if (value >= 2 && value <= 5) {
          gradeLit = value;
        } else {
          gradeLit = 1;
        }
      }
    }

    public override string ToString() {
      string returnstring;

      returnstring = $"{LastName}\n{FirstName}\nSchool number: {schoolNumber}\n" +
        $"Grade math: {gradeMath}\nGrade literature: {gradeLit}\n";

      return returnstring;
    }
  }
}
