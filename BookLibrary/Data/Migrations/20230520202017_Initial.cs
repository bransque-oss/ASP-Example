﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanChangeEntities = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Andrew Lock graduated with an Engineering degree from Cambridge University, specializing in Software Engineering, and went on to obtain a PhD in Digital Image Processing. He has been developing professionally using .NET for the last 10 years, using a wide range of technologies including WinForms, ASP.NET WebForms, ASP.NET MVC, and ASP.NET Webpages.\r\n\r\nHe has been building and maintaining applications with ASP.NET Core since the release of 1.0 in 2016. Andrew has a very active blog at https://andrewlock.net dedicated to ASP.NET Core. It is frequently featured in the community spotlight by the ASP.NET team at Microsoft, on the .NET blog, and in the weekly community standups. Andrew has been a Microsoft Valued Professional (MVP) since 2017, and is author of the book ASP.NET Core in Action.", "Andrew Lock" },
                    { 2, "Эрих Мария Pемарк - знаменитый писатель XX века. Родился в г. Oснабрюк (Германия) 22 июня 1898 года в семье переплетчика Петера Франца Pемарк и Анны Марии Шталькнеxт. В юности увлекался творчеством Томаса Манна, Цвейга, Пруста, Достоевского и Гете. После церковной школы в 1915 году поступил в католическую семинарию, но закончить не успел, через год его призвали в армию, а в 1917 году направили на Западный фронт, где он был ранен и остаток войны провел в военном госпитале. В 1919-1921 годах работал сначала учителем, а затем сменил несколько профессий: побывал и в роли воскресного органиста, и продавца надгробных памятников. События этого периода позже легли в основу его произведения \"Черный обелиск\".", "Эрих Мария Ремарк" },
                    { 3, "Дуглас Ноэль Адамс — английский писатель, драматург и сценарист, автор юмористических фантастических произведений. Известен как создатель знаменитой серии книг «Автостопом по галактике». Адамс получил степень бакалавра и магистра по специализации \"Английская литература\". Он начал публиковаться, будучи еще школьником. Его творческая деятельность не ограничивается лишь писательством. Адамс участвует в эпизодах скетч-шоу \"Монти Пайтон\", пишет сценарии для телеканала BBC2, музицирует. 1957 - переезд из Кэмбриджа в Брентвуд, Эссекс. 1959-1570 - школа Праймроуз Хилл 1962 - публикация в школьном журнале 1965 - публикация в журнале для мальчиков 1971 - поступление в колледж Св. Джона, Кэмбридж 1974 - получение степени бакалавра, английская литература 1974 - сотрудничество с телевидением середина 70-х гг. - сложности с работой. 1978 - первое появление \"Автостопом по галактике\" на радио. Первая серия из задуманных четырех цикла рассказов для радио 1978 - пишет сценарий эпизода The Pirate Planet сюжетной арки Key to Time в телевизионном сериале \"Доктор Кто\" 1979 - соавторство в сценарии для двух серий телевизионного сериала \"Доктор Снагглс\" 1979 - соавторство в сценарии для четырех эпизодов серии City of Death телевизионного сериала \"Доктор Кто\" 1980 - выпуск на радио второй серии \"Автостопом по галактике\" 1979-1992 - появление \"Автостопом по галактике\" в привычном сегодня виде книг. 1980 - начала долгого процесса экранизации \"Путеводителя\" 1981 - на канал BBC Two выходят шесть эпизодов сериала \"Путеводитель по Галактике для автостопщиков\" 2005 - выход фильма \"Автостопом по галактике\" на широкие экраны. Дуглас Адамс этого уже не увидел.", "Дуглас Адамс" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CanChangeEntities", "Login", "Password" },
                values: new object[,]
                {
                    { 1, true, "admin", "3627909a29c31381a071ec27f7c9ca97726182aed29a7ddd2e54353322cfb30abb9e3a6df2ac2c20fe23436311d678564d0c8d305930575f60e2d3d048184d79" },
                    { 2, false, "someuser", "3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Isbn", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Summary\r\n\r\nASP.NET Core in Action is for C# developers without any web development experience who want to get started and productive fast using ASP.NET Core 2.0 to build web applications.\r\n\r\nPurchase of the print book includes a free eBook in PDF, Kindle, and ePub formats from Manning Publications.\r\n\r\nAbout the Technology\r\n\r\nThe dev world has permanently embraced open platforms with flexible tooling, and ASP.NET Core has changed with it. This free, open source web framework delivers choice without compromise. You can enjoy the benefits of a mature, well-supported stack and the freedom to develop and deploy from and onto any cloud or on-prem platform.\r\n\r\nAbout the Book\r\n\r\nASP.NET Core in Action opens up the world of cross-platform web development with .NET. You'll start with a crash course in .NET Core, immediately cutting the cord between ASP.NET and Windows. Then, you'll begin to build amazing web applications step by step, systematically adding essential features like logins, configuration, dependency injection, and custom components. Along the way, you'll mix in important process steps like testing, multiplatform deployment, and security.\r\n\r\nWhat's Inside\r\nCovers ASP.NET Core 2.0\r\nDynamic page generation with the Razor templating engine\r\nDeveloping ASP.NET Core apps for non-Windows servers\r\nClear, annotated examples in C#\r\n", "978-1617294617", "ASP.NET Core In Action" },
                    { 2, 2, "Трое друзей - Робби, отчаянный автогонщик Кестер и \"последний романтик\" Лени прошли Первую мировую войну. Вернувшись в гражданскую жизнь, они основали небольшую автомастерскую. И хотя призраки прошлого преследуют их, они не унывают - ведь что может быть лучше дружбы, крепкой и верной, ради которой можно отдать последнее? Наверное, лишь только любовь, не знающая границ и пределов. Прекрасная и грустная Пат, нежная возлюбленная Робби, рассеивает мрак бессмысленности его существования. Однако обретенному счастью угрожают отголоски все той же войны - существующие уже не только в памяти и сознании героев, а суровым образом воплотившиеся в реальность... Эта история раз и навсегда покорила сердца миллионов читателей по всему миру. На этой книге выросли поколения, ее давно раздергали на цитаты, неоднократно экранизировали и продолжают ставить на сцене. Ее хочется перечитывать снова и снова.", "978-5-17-082358-1", "Три товарища" },
                    { 3, 2, "Говоря о Первой мировой войне, всегда вспоминают одно произведение Эриха Марии Ремарка.\r\n\"На Западном фронте без перемен\".\r\nЭто рассказ о немецких мальчишках, которые под действием патриотической пропаганды идут на войну, не зная о том, что впереди их ждет не слава героев, а инвалидность и смерть… \r\nКаждый день войны уносит жизни чьих-то отцов, сыновей, а газеты тем временем бесстрастно сообщают: \"На Западном фронте без перемен...\".\r\nЭта книга - не обвинение, не исповедь. \r\nЭто попытка рассказать о поколении, которое погубила война, о тех, кто стал ее жертвой, даже если сумел спастись от снарядов и укрыться от пули.", "978-5-17-108431-8", "На западном фронте без перемен" },
                    { 4, 3, "В этой книге читатель вновь встретится с полюбившимися героями: среднестатистическим приспособленцем Артуром Дентом, сотрудником межгалактического издательства Фордом Префектом, продвинутой барышней Триллиан, депрессивным роботом Марвином, - и отправится вместе с ними в новые странствия по бесчисленным мирам, наполненным опасными приключениями и непредсказуемыми сюрпризами!", "978-5-17-085834-7", "Автостопом по галактике" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
