using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Linq;

namespace ScanProjectManagement.Business
{
    public static class MailMessageExtensions
    {
        public static void RawMessage(this MailMessage m)
        {
            var smtpClient = new SmtpClient { DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory };

            var tempDir = new TemporaryDirectory();

            smtpClient.PickupDirectoryLocation = tempDir.DirectoryPath;
            smtpClient.Send(m);
        }
    }

    public class MeetingInvite
    {
        public static MailMessage CreateMeetingRequest(DateTime start, DateTime end, string subject, string summary,
            string location, string organizerName, string organizerEmail, string attendeeName, string attendeeEmail)
        {
            MailAddressCollection col = new MailAddressCollection();
            col.Add(new MailAddress(attendeeEmail, attendeeName));
            return CreateMeetingRequest(start, end, subject, summary, location, organizerName, organizerEmail, col);
        }

        public static MailMessage CreateMeetingRequest(DateTime start, DateTime end, string subject, string summary,
            string location, string organizerName, string organizerEmail, MailAddressCollection attendeeList)
        {
            MailMessage msg = new MailMessage();

            //mime types contained in the message
            ContentType textType = new ContentType("text/plain");
            ContentType HTMLType = new ContentType("text/html");
            ContentType calendarType = new ContentType("text/calendar");

            //  Add parameters to header
            calendarType.Parameters.Add("method", "REQUEST");
            calendarType.Parameters.Add("name", "meeting.ics");

            //  Create message body parts, create the Body in text format
            string bodyText = "Type:Single Meeting\r\nOrganizer: {0}\r\nStart Time:{1}\r\nEnd Time:{2}\r\nTime Zone:{3}\r\nLocation: {4}\r\n\r\n*~*~*~*~*~*~*~*~*~*\r\n\r\n{5}";
            bodyText = string.Format(bodyText,
                organizerName,
                start.ToLongDateString() + " " + start.ToLongTimeString(),
                end.ToLongDateString() + " " + end.ToLongTimeString(),
                TimeZone.CurrentTimeZone.StandardName,
                location,
                summary);

            AlternateView textView = AlternateView.CreateAlternateViewFromString(bodyText, textType);
            msg.AlternateViews.Add(textView);

            //create the Body in HTML format
            string bodyHTML = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\">\r\n<HTML>\r\n<HEAD>\r\n<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">\r\n<META NAME=\"Generator\" CONTENT=\"MS Exchange Server version 6.5.7652.24\">\r\n<TITLE>{0}</TITLE>\r\n</HEAD>\r\n<BODY>\r\n<!-- Converted from text/plain format -->\r\n<P><FONT SIZE=2>Type:Single Meeting<BR>\r\nOrganizer:{1}<BR>\r\nStart Time:{2}<BR>\r\nEnd Time:{3}<BR>\r\nTime Zone:{4}<BR>\r\nLocation:{5}<BR>\r\n<BR>\r\n*~*~*~*~*~*~*~*~*~*<BR>\r\n<BR>\r\n{6}<BR>\r\n</FONT>\r\n</P>\r\n\r\n</BODY>\r\n</HTML>";
            bodyHTML = string.Format(bodyHTML,
                summary,
                organizerName,
                start.ToLongDateString() + " " + start.ToLongTimeString(),
                end.ToLongDateString() + " " + end.ToLongTimeString(),
                TimeZone.CurrentTimeZone.StandardName,
                location,
                summary);

            AlternateView HTMLView = AlternateView.CreateAlternateViewFromString(bodyHTML, HTMLType);
            msg.AlternateViews.Add(HTMLView);

            //create the Body in VCALENDAR format
            string calDateFormat = "yyyyMMddTHHmmssZ";
            string bodyCalendar = "BEGIN:VCALENDAR\r\nMETHOD:REQUEST\r\nPRODID:Microsoft CDO for Microsoft Exchange\r\nVERSION:2.0\r\nBEGIN:VTIMEZONE\r\nTZID:(GMT-06.00) Central Time (US & Canada)\r\nX-MICROSOFT-CDO-TZID:11\r\nBEGIN:STANDARD\r\nDTSTART:16010101T020000\r\nTZOFFSETFROM:-0500\r\nTZOFFSETTO:-0600\r\nRRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=11;BYDAY=1SU\r\nEND:STANDARD\r\nBEGIN:DAYLIGHT\r\nDTSTART:16010101T020000\r\nTZOFFSETFROM:-0600\r\nTZOFFSETTO:-0500\r\nRRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=3;BYDAY=2SU\r\nEND:DAYLIGHT\r\nEND:VTIMEZONE\r\nBEGIN:VEVENT\r\nDTSTAMP:{8}\r\nDTSTART:{0}\r\nSUMMARY:{7}\r\nUID:{5}\r\nATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"{9}\":MAILTO:{9}\r\nACTION;RSVP=TRUE;CN=\"{4}\":MAILTO:{4}\r\nORGANIZER;CN=\"{3}\":mailto:{4}\r\nLOCATION:{2}\r\nDTEND:{1}\r\nDESCRIPTION:{7}\\N\r\nSEQUENCE:1\r\nPRIORITY:5\r\nCLASS:\r\nCREATED:{8}\r\nLAST-MODIFIED:{8}\r\nSTATUS:CONFIRMED\r\nTRANSP:OPAQUE\r\nX-MICROSOFT-CDO-BUSYSTATUS:BUSY\r\nX-MICROSOFT-CDO-INSTTYPE:0\r\nX-MICROSOFT-CDO-INTENDEDSTATUS:BUSY\r\nX-MICROSOFT-CDO-ALLDAYEVENT:FALSE\r\nX-MICROSOFT-CDO-IMPORTANCE:1\r\nX-MICROSOFT-CDO-OWNERAPPTID:-1\r\nX-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:{8}\r\nX-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:{8}\r\nBEGIN:VALARM\r\nACTION:DISPLAY\r\nDESCRIPTION:REMINDER\r\nTRIGGER;RELATED=START:-PT00H15M00S\r\nEND:VALARM\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n";
            bodyCalendar = string.Format(bodyCalendar,
                start.ToUniversalTime().ToString(calDateFormat),
                end.ToUniversalTime().ToString(calDateFormat),
                location,
                organizerName,
                organizerEmail,
                Guid.NewGuid().ToString("B"),
                summary,
                subject,
                DateTime.Now.ToUniversalTime().ToString(calDateFormat),
                attendeeList.ToString());

            AlternateView calendarView = AlternateView.CreateAlternateViewFromString(bodyCalendar, calendarType);
            calendarView.TransferEncoding = TransferEncoding.SevenBit;
            msg.AlternateViews.Add(calendarView);

            //  Adress the message
            msg.From = new MailAddress(organizerEmail);
            foreach (MailAddress attendee in attendeeList)
                msg.To.Add(attendee);

            msg.Subject = subject;
            return msg;
        }
    }

    internal class TemporaryDirectory
    {
        private static string _storage = string.Empty;
        private static string meetingInviteStorage
        {
            get
            {
                if (string.IsNullOrEmpty(_storage))
                {
                    _storage = configurationHelper.getMeetingInviteStorage();
                }
                return _storage;
            }
        }

        private static string _random = string.Empty;
        private static string randomDir
        {
            get
            {
                if (string.IsNullOrEmpty(_random))
                {
                    _random = Path.GetRandomFileName();
                }
                return _random;
            }
        }

        public TemporaryDirectory()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
        }

        public string DirectoryPath
        {
            get
            {
                return Path.Combine(meetingInviteStorage, randomDir);
            }
        }
    }
}