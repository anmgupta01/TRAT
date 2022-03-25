using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;

namespace TRAT
{
    public class EmailManager
    {
        public void SendMail(string toAddress, string fromAddress, string subject, string body, string bccAddress, string imagePath, string fromAddressDisplayName = null)
        {
            var refNum = Guid.NewGuid();
            try
            {
                //_logger.Debug($"Send Email request received from {fromAddress} to {toAddress} subject: {subject} at: {DateTime.Now} log reference number: {refNum}");
                if (!string.IsNullOrEmpty(fromAddress) && !string.IsNullOrEmpty(toAddress))
                {
                    var fromMailAddr = !string.IsNullOrEmpty(fromAddressDisplayName) ? new MailAddress(fromAddress, fromAddressDisplayName) : new MailAddress(fromAddress);
                    var toMailAddr = new MailAddress(toAddress);
                    var message = new MailMessage(fromMailAddr, toMailAddr)
                    {
                        IsBodyHtml = true,
                        Subject = subject
                    };

                    if (!string.IsNullOrWhiteSpace(bccAddress))
                        message.Bcc.Add(new MailAddress(bccAddress));


                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var inlineLogo = new Attachment(imagePath)
                        {
                            ContentId = "imageId"
                        };
                        inlineLogo.ContentDisposition.Inline = true;
                        message.Attachments.Add(inlineLogo);
                    }

                    message.Body = body;
                    var smtpSender = new SmtpClient();
                    smtpSender.Send(message);
                    message.Dispose();
                    smtpSender.Dispose();
                }

            }
            catch (Exception ex)
            {
            }
        }
        public async Task SendMailAsync(string toAddress, string fromAddress, string subject, string body, string bccAddress, string imagePath, string fromAddressDisplayName = null)
        {
            await Task.Run(() => SendMail(toAddress, fromAddress, subject, body, bccAddress, imagePath, fromAddressDisplayName));
        }
        public void SendMail(string toAddress, string fromAddress, string subject, string body, string bccAddress, string imagePath, DataSet ds, string fromAddressDisplayName = null)
        {
            var refNum = Guid.NewGuid();
            try
            {
                //_logger.Debug($"Send Email request received from {fromAddress} to {toAddress} subject: {subject} at: {DateTime.Now} log reference number: {refNum}");
                if (!string.IsNullOrEmpty(fromAddress) && !string.IsNullOrEmpty(toAddress))
                {
                    var fromMailAddr = !string.IsNullOrEmpty(fromAddressDisplayName) ? new MailAddress(fromAddress, fromAddressDisplayName) : new MailAddress(fromAddress);
                    var toMailAddr = new MailAddress(toAddress);
                    var message = new MailMessage(fromMailAddr, toMailAddr)
                    {
                        IsBodyHtml = true,
                        Subject = subject
                    };

                    if (!string.IsNullOrWhiteSpace(bccAddress))
                        message.Bcc.Add(new MailAddress(bccAddress));


                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var inlineLogo = new Attachment(imagePath)
                        {
                            ContentId = "imageId"
                        };
                        inlineLogo.ContentDisposition.Inline = true;
                        message.Attachments.Add(inlineLogo);
                    }
                    if (ds.Tables.Count > 0)
                    {
                        MemoryStream mstream = new MemoryStream();
                        Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                        Font title = new Font(Font.FontFamily.TIMES_ROMAN, 15f, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        Font header = new Font(Font.FontFamily.TIMES_ROMAN, 13f, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
                        PdfWriter writer = PdfWriter.GetInstance(document, mstream);
                        Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                        document.Open();
                        document.Add(new Paragraph("Workorder", title));
                        foreach (DataTable dt in ds.Tables)
                        {
                            document.Add(p);
                            document.Add(new Paragraph(dt.TableName, header));
                            document.Add(p);
                            if (dt.Rows.Count > 0)
                            {

                                foreach (DataColumn dc in dt.Columns)
                                {
                                    document.Add(new Paragraph(dc.ColumnName + "  :  " + dt.Rows[0][dc.ColumnName]));
                                }
                            }
                        }
                        document.Close();
                        MemoryStream pdfstream = new MemoryStream(mstream.ToArray());
                        message.Attachments.Add(new Attachment(pdfstream, "WorkOrder.pdf"));
                    }


                    message.Body = body;
                    //var smtpSender = new SmtpClient("ausmtp");
                    var smtpSender = new SmtpClient("inappmail.atrapa.deloitte.com");

                    smtpSender.Send(message);
                    message.Dispose();
                    smtpSender.Dispose();
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void SendMailWithAttachment(string toAddress, string fromAddress, string subject, string body, string ccAddress, string imagePath, DataSet ds, ReportViewer reportViewer, string fromAddressDisplayName = null)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;


            string encoding;
            string extension;
            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension,out streamids, out warnings);
            var refNum = Guid.NewGuid();
            try
            {
                if (!string.IsNullOrEmpty(fromAddress) && !string.IsNullOrEmpty(toAddress))
                {
                    MailMessage mailMessage = new MailMessage();
                    var fromMailAddr = !string.IsNullOrEmpty(fromAddressDisplayName) ? new MailAddress(fromAddress, fromAddressDisplayName) : new MailAddress(fromAddress);
                    mailMessage.From = fromMailAddr;
                    foreach (var address in toAddress.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.To.Add(address);
                    }
                    foreach (var address in ccAddress.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.CC.Add(address);
                    }
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = subject;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var inlineLogo = new Attachment(imagePath)
                        {
                            ContentId = "imageId"
                        };
                        inlineLogo.ContentDisposition.Inline = true;
                        mailMessage.Attachments.Add(inlineLogo);
                    }
                    MemoryStream memoryStream = new MemoryStream(bytes);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    Attachment attachment = new Attachment(memoryStream, "WorkOrderDetails.PDF");
                    mailMessage.Attachments.Add(attachment);
                    mailMessage.Body = body.Replace(Environment.NewLine, "<br/>"); ;
                    //var smtpSender = new SmtpClient("ausmtp");
                    var smtpSender = new SmtpClient("inappmail.atrapa.deloitte.com");
                    smtpSender.Send(mailMessage);
                    mailMessage.Dispose();
                    smtpSender.Dispose();
                    
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void SendMailWith_MultipleAttachment(string toAddress, string fromAddress, string subject, string body, string ccAddress, string imagePath, DataSet ds, ReportViewer reportViewer,string fromAddressDisplayName = null)
        {

            Warning[] warnings;
            string[] streamids;
            string mimeType;


            string encoding;
            string extension;
            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            var refNum = Guid.NewGuid();
            try
            {
                if (!string.IsNullOrEmpty(fromAddress) && !string.IsNullOrEmpty(toAddress))
                {
                    MailMessage mailMessage = new MailMessage();
                    var fromMailAddr = !string.IsNullOrEmpty(fromAddressDisplayName) ? new MailAddress(fromAddress, fromAddressDisplayName) : new MailAddress(fromAddress);
                    mailMessage.From = fromMailAddr;
                    foreach (var address in toAddress.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.To.Add(address);
                    }
                    foreach (var address in ccAddress.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mailMessage.CC.Add(address);
                    }
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = subject;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var inlineLogo = new Attachment(imagePath)
                        {
                            ContentId = "imageId"
                        };
                        inlineLogo.ContentDisposition.Inline = true;
                        mailMessage.Attachments.Add(inlineLogo);
                    }
                    MemoryStream memoryStream = new MemoryStream(bytes);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    Attachment attachment = new Attachment(memoryStream, "EPCC.PDF");
                    mailMessage.Attachments.Add(attachment);

                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        MemoryStream memoryStream_Temp = new MemoryStream((Byte[])dr["Data"]);
                        memoryStream_Temp.Seek(0, SeekOrigin.Begin);
                        Attachment attachment_Temp = new Attachment(memoryStream_Temp, dr["Name"].ToString());
                        mailMessage.Attachments.Add(attachment_Temp);
                    }


                    mailMessage.Body = body.Replace(Environment.NewLine, "<br/>"); ;
                    //var smtpSender = new SmtpClient("ausmtp");
                    var smtpSender = new SmtpClient("inappmail.atrapa.deloitte.com");
                    smtpSender.Send(mailMessage);
                    mailMessage.Dispose();
                    smtpSender.Dispose();

                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}