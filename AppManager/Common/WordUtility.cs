using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MSWord = Microsoft.Office.Interop.Word;
namespace AppManager.Common
{
    public class WordUtility
    {
        public static string OptionTemplate = "A,{0}   B,{1}   C,{2}   D,{3}\n";
        public static string OptionTemplate2 = "A,{0}   B,{1} \n  C,{2}   D,{3}\n";
        public static string OptionTemplate3 = "A,{0} \n B,{1} \n C,{2}\n D,{3}\n";

        public async static void CreateTestPaperWord(TestPaper testPaper)
        {

            object path;                              //文件路径变量
            string strContent;                        //文本内容变量
            MSWord.Application wordApp;                   //Word应用程序变量 
            MSWord.Document wordDoc;                  //Word文档变量
            string pathStr = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\QBM\";
            Directory.CreateDirectory(pathStr);
            wordApp = new MSWord.Application(); //初始化
            pathStr = pathStr + "TestPaper" + testPaper.Id + ".doc";
            wordApp.Visible = false;//使文档可见
            path = pathStr;
            //如果已存在，则删除
            if (!File.Exists((string)path))
            {


                //由于使用的是COM库，因此有许多变量需要用Missing.Value代替
                Object Nothing = Missing.Value;
                wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                #region 页面设置、页眉图片和文字设置，最后跳出页眉设置
                //页面设置
                wordDoc.PageSetup.PaperSize = MSWord.WdPaperSize.wdPaperA4;//设置纸张样式为A4纸
                wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientPortrait;//排列方式为垂直方向
                wordDoc.PageSetup.TopMargin = 57.0f;
                wordDoc.PageSetup.BottomMargin = 57.0f;
                wordDoc.PageSetup.LeftMargin = 57.0f;
                wordDoc.PageSetup.RightMargin = 57.0f;
                wordDoc.PageSetup.HeaderDistance = 30.0f;//页眉位置
                #endregion

                #region 页码设置并添加页码
                //为当前页添加页码
                MSWord.PageNumbers pns = wordApp.Selection.Sections[1].Headers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;//获取当前页的号码
                pns.NumberStyle = MSWord.WdPageNumberStyle.wdPageNumberStyleNumberInDash;//设置页码的风格，是Dash形还是圆形的
                pns.HeadingLevelForChapter = 0;
                pns.IncludeChapterNumber = false;
                pns.RestartNumberingAtSection = false;
                pns.StartingNumber = 1; //开始页页码
                object pagenmbetal = MSWord.WdPageNumberAlignment.wdAlignPageNumberCenter;//将号码设置在中间
                object first = true;
                wordApp.Selection.Sections[1].Footers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);
                #endregion
                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
                wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度
                wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
                object unite = MSWord.WdUnits.wdStory;
                wordApp.Selection.EndKey(ref unite, ref Nothing);
                strContent = testPaper.Title + "\n";
                wordDoc.Paragraphs.Last.Range.Font.Size = 20;
                wordDoc.Paragraphs.Last.Range.Font.Name = "黑体";
                wordDoc.Paragraphs.Last.Range.Text = strContent;
                wordApp.Selection.EndKey(ref unite, ref Nothing);
                wordDoc.Paragraphs.Last.Range.Font.Size = 16;
                strContent = testPaper.Subtitle + "\n";
                wordDoc.Paragraphs.Last.Range.Font.Underline = MSWord.WdUnderline.wdUnderlineThick;
                wordDoc.Paragraphs.Last.Range.Text = strContent;
                int paperQuestionTypeNumber = 1;
                wordApp.Selection.EndKey(ref unite, ref Nothing);
                wordDoc.Paragraphs.Last.Range.Font.Underline = MSWord.WdUnderline.wdUnderlineNone;
                foreach (var paperQuestionType in testPaper.PaperQuestionTypes)
                {
                    wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphLeft;
                    strContent = paperQuestionTypeNumber+ "、" + paperQuestionType.PaperQuestionTitle + "\n";
                    bool hasOption = paperQuestionType.QuestionType.HasOption;
                    wordApp.Selection.EndKey(ref unite, ref Nothing);
                    wordDoc.Paragraphs.Last.Range.Font.Size = 14;
                    wordDoc.Paragraphs.Last.Range.Font.Name = "黑体";
                    wordDoc.Paragraphs.Last.Range.Text = strContent;
                    int examinationQuestionNumber = 1;
                    foreach (var examinationQuestion in paperQuestionType.ExaminationQuestionPaperTypes)
                    {
                        strContent = examinationQuestionNumber + "、" + examinationQuestion.ExaminationQuestion.Content + "\n";
                        wordApp.Selection.EndKey(ref unite, ref Nothing);
                        wordDoc.Paragraphs.Last.Range.Font.Size = 12;
                        wordDoc.Paragraphs.Last.Range.Font.Name = "宋体";
                        if (hasOption)
                        {
                            wordDoc.Paragraphs.Last.Range.Text = strContent;
                            wordApp.Selection.EndKey(ref unite, ref Nothing);
                            OptionItem[] optionItems = examinationQuestion.ExaminationQuestion.OptionItems.ToArray();
                            wordDoc.Paragraphs.Last.Range.Font.Size = 10;
                            if (optionItems[0].OptionContent.Length > 45 || optionItems[1].OptionContent.Length > 45 || optionItems[2].OptionContent.Length > 45 || optionItems[3].OptionContent.Length > 45)
                            {
                                wordDoc.Paragraphs.Last.Range.Text = String.Format(OptionTemplate3, optionItems[0].OptionContent, optionItems[1].OptionContent, optionItems[2].OptionContent, optionItems[3].OptionContent);

                            }
                            else if (optionItems[0].OptionContent.Length > 15 || optionItems[1].OptionContent.Length > 15 || optionItems[2].OptionContent.Length > 15 || optionItems[3].OptionContent.Length > 15)
                            {
                                wordDoc.Paragraphs.Last.Range.Text = String.Format(OptionTemplate2, optionItems[0].OptionContent, optionItems[1].OptionContent, optionItems[2].OptionContent, optionItems[3].OptionContent);
                            }
                            else
                            {
                                wordDoc.Paragraphs.Last.Range.Text = String.Format(OptionTemplate, optionItems[0].OptionContent, optionItems[1].OptionContent, optionItems[2].OptionContent, optionItems[3].OptionContent);
                            }
                        }
                        else
                        {
                            wordDoc.Paragraphs.Last.Range.Text = strContent;
                        }
                        
                        examinationQuestionNumber++;
                    }
                    paperQuestionTypeNumber++;
                }
                object format = MSWord.WdSaveFormat.wdFormatDocument;// office 2007就是wdFormatDocumentDefault
                                                                     //将wordDoc文档对象的内容保存为DOCX文档
                wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                //关闭wordApp组件对象
                wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);

            }
            OpenWord(path);
            

        }
        public async static void PrintWord(object path)
        {
            MSWord.Application wordApp = new MSWord.Application();
            Object Nothing = Missing.Value;
            MSWord.Document wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            MSWord.Document doc = null;
            try
            {
                object unknow = Type.Missing;
                wordApp.Visible = false;

                object file = path;
                doc = wordApp.Documents.Open(ref file,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow);
                wordDoc.PrintOut();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async static void OpenWord(object path)
        {
            Object Nothing = Missing.Value;
            MSWord.Application app = new MSWord.Application();
            MSWord.Document doc = null;
            try
            {
                object unknow = Type.Missing;
                app.Visible = true;

                object file = path;
                doc = app.Documents.Open(ref file,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
