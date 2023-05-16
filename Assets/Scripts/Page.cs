using System.Collections;
using System.Collections.Generic;


public class Page
{
    public string Title { get; set; }
    public string Text { get; set; }
    public List<string> Pages { get; set; }

    private static List<Page> _pageList = null;

    public static Page RandomPage;

    public static int CurrentPage1 = 0;
    public static int CurrentPage2 = 1;

    public static Page GetRandomPage()
    {  
        List<Page> pageList = Page.PageList;

        int num = UnityEngine.Random.Range(0, pageList.Count);
        Page pge = pageList[num];
        pge.Pages = new List<string>();

        string[] words = pge.Text.Split(' ');
        string page = "";
        int wordCnt = 0;

        foreach (string word in words)
        {
            wordCnt++;
            if (wordCnt > 6)
            {
                pge.Pages.Add(page);
                page = "";
                wordCnt = 0;
            }
            page += string.Format("{0}", word);
        }
        pge.Pages.Add(page);

        RandomPage = pge;

        return pge;
    }

    public static List<Page> PageList
    {
        get
        {
            if (_pageList == null)
            {
                _pageList = new List<Page>();

                _pageList.Add(new Page
                {   //1
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //2
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //3
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //4
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //5
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //6
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //7
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //8
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });

                _pageList.Add(new Page
                {   //9
                    Title = "Acts 17:11",
                    Text = "aaaaa"
                });
            }

            return _pageList;
        }
    }
}


