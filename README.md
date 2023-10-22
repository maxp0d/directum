# Добро пожаловать!
Этот репозиторий содержит мои решения заданий для кандидатов на прикладную разработку (С#).
# Задание 1. Проектирование баз данных
## Вариант 1
Спроектирована база из 5 таблиц:
```
CREATE TABLE `students` (
	`id` INT,
	`FirstName` varchar(32),
	`LastName` varchar(32),
	`CourseListId` INT,
	PRIMARY KEY (`id`)
);
```

```
CREATE TABLE `courses` (
	`id` INT,
	`Name` varchar(64),
	`Capacity` INT DEFAULT '0' CHECK (ActualCourse),
	PRIMARY KEY (`id`),
  FOREIGN KEY (`ListOfStudens`) REFERENCES `studentsoncourse`(`id`),
  FOREIGN KEY (`ActualCourse`) REFERENCES `timetable`(`ActualCourse`)
);
```
```
CREATE TABLE `mycourses` (
	`id` INT ,
	`already_learning` INT CHECK (already_learningn >=0 OR already_learning<=1),
	PRIMARY KEY (`id`),
  FOREIGN KEY (`course_id`) REFERENCES `courses`(`id`)
);
```
```
CREATE TABLE `studentsoncourse` (
	`id` INT,
	PRIMARY KEY (`id`)
  FOREIGN KEY (`student_id`) REFERENCES `students`(`id`)
);
```
```
CREATE TABLE `timetable` (
	`id` INT ,
	`actual_course` bool,
	`date` DATETIME,
	PRIMARY KEY (`id`),
  FOREIGN KEY (`course_id`) REFERENCES `courses`(`id`),
  FOREIGN KEY (`actual_course`) REFERENCES `courses`(`ActualCourse`)
);
```

# Задание 2
## Вариант 1. Разработка карточки сотрудника.
Визуальная форма для просмотра и редактирования сотрудников реализована на четырех вкладках: [Основные сведения][Адреса][Предыдущие места работы][Состав семьи].
Обязательные поля помечены знаком (*). Форму невозможно сохранить пока все обязательные поля не будут заполнены. Предусмотрена возможность сохранить неполностью заполненную форму как черновик для дальнейшего редактирования.
Вкладки [Предыдущие места работы][Состав семьи] содержат таблицы с соответствующим содержимым. В каждой таблице можно добавлять/редактировать/удалять записи.

![Tab1](https://github.com/maxp0d/directum/blob/master/Task2%20GUI/Tab1.jpg)

![Tab2](https://github.com/maxp0d/directum/blob/master/Task2%20GUI/Tab2.jpg)

![Tab3](https://github.com/maxp0d/directum/blob/master/Task2%20GUI/Tab3.jpg)

![Tab4](https://github.com/maxp0d/directum/blob/master/Task2%20GUI/Tab4.jpg)
# Задание 3
