using System;

namespace lab1_n2_2 {
  public static class RandomData {
    private static string[] randomLastName = {
      "Ivanov",
      "Sergeev",
      "Koshev",
      "Kamenev",
      "Sidorov"
    };

    private static string[] randomFirstName = {
      "Ivan",
      "Genadiy",
      "Fedor",
      "Sergey",
      "Konstantin"
    };

    public static ProfileStudent setRandomValues() {
      ProfileStudent profileStudent = new ProfileStudent();

      profileStudent.LastName = randomLastName[generateRandomNumber() - 1];
      profileStudent.FirstName = randomFirstName[generateRandomNumber() - 1];
      profileStudent.SchoolNumber = generateRandomNumber();
      profileStudent.GradeMath = generateRandomNumber();
      profileStudent.GradeLit = generateRandomNumber();

      return profileStudent;
    }

    private static int generateRandomNumber() {
      Random random = new Random(Guid.NewGuid().GetHashCode());
      
      return random.Next(1, 6);
    }
  }
}
