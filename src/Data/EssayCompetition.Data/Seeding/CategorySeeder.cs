namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Title, string Description, string ImageUrl)>()
            {
                ("Narrative Essays: Telling a Story", "In a narrative essay, the writer tells a story about a real-life experience. While telling a story may sound easy to do, the narrative essay challenges students to think and write about themselves. When writing a narrative essay, writers should try to involve the reader by making the story as vivid as possible. The fact that narrative essays are usually written in the first person helps engage the reader. “I” sentences give readers a feeling of being part of the story. A well-crafted narrative essay will also build towards drawing a conclusion or making a personal statement.", "https://www.makemyassignments.com/blog/wp-content/uploads/2018/12/images-3.jpg"),
                ("Descriptive Essays: Painting a Picture", "A cousin of the narrative essay, a descriptive essay paints a picture with words. A writer might describe a person, place, object, or even memory of special significance. However, this type of essay is not description for description’s sake. The descriptive essay strives to communicate a deeper meaning through the description. In a descriptive essay, the writer should show, not tell, through the use of colorful words and sensory details. The best descriptive essays appeal to the reader’s emotions, with a result that is highly evocative.", "https://thesiseditor.co.uk/wp-content/uploads/2017/10/descriptive-essays.png"),
                ("Expository Essays: Just the Facts", "The expository essay is an informative piece of writing that presents a balanced analysis of a topic. In an expository essay, the writer explains or defines a topic, using facts, statistics, and examples. Expository writing encompasses a wide range of essay variations, such as the comparison and contrast essay, the cause and effect essay, and the “how to” or process essay. Because expository essays are based on facts and not personal feelings, writers don’t reveal their emotions or write in the first person.", "https://academichelp.net/wp-content/uploads/2012/02/expository-224x300.jpg"),
                ("Persuasive Essays: Convince Me", "While like an expository essay in its presentation of facts, the goal of the persuasive essay is to convince the reader to accept the writer’s point of view or recommendation. The writer must build a case using facts and logic, as well as examples, expert opinion, and sound reasoning. The writer should present all sides of the argument, but must be able to communicate clearly and without equivocation why a certain position is correct.", "https://images-na.ssl-images-amazon.com/images/I/513xEYLa4EL._SX378_BO1,204,203,200_.jpg"),
            };

            var currentCategory = default(Category);
            foreach (var category in categories)
            {
                currentCategory = new Category()
                {
                    Title = category.Title,
                    Description = category.Description,
                    ImageUrl = category.ImageUrl,
                };
                await dbContext.AddAsync(currentCategory);
            }
        }
    }
}
