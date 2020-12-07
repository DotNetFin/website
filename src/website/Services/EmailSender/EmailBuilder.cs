using System.Text;

namespace website.Services.EmailSender
{
    public class EmailBuilder
    {
        private StringBuilder builder = new StringBuilder(@"
			<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
				<html xmlns=""http://www.w3.org/1999/xhtml"">
					<head>
						<title>DotNetFin</title>
						<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8""/>
						<meta name=""x-apple-disable-message-reformatting""/>
						<meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>	
						<style type=""text/css"">

							/* Outlook link fix */
							#outlook a {
								padding: 0;
							}

							/* Hotmail background & line height fixes */
							.ExternalClass {
								width: 100% !important;
							}

							.ExternalClass,
							.ExternalClass p,
							.ExternalClass span,
							.ExternalClass font,

							/* Image borders & formatting */
							img {
								outline: none;
								text-decoration: none;
								-ms-interpolation-mode: bicubic;
							}

							a img {
								border: none;
							}

							/* Re-style iPhone automatic links (eg. phone numbers) */
							.appleLinksGrey a {
								color: #05202D !important;
								text-decoration: none !important;
							}

							/* Hotmail symbol fix for mobile devices */
							.ExternalClass img[class^=Emoji] {
								width: 10px !important;
								height: 10px !important;
								display: inline !important;
							}
		
							/* Button hover colour change */
							.CTA:hover {
								background-color: #003580 !important;
							}

							@media screen and (max-width:640px) {
								.mobilefullwidth {
									width: 100% !important;
									height: auto !important;
								}

								.logo {
									padding-left: 20px !important;
									padding-right: 20px !important;
								}

								.h1 {
									font-size: 36px !important;
									line-height: 43px !important;
									padding-right: 25px !important;
									padding-left: 25px !important;
									padding-top: 25px !important;
								}

								.h2 {
									font-size: 18px !important;
									line-height: 27px !important;
									padding-right: 25px !important;
									padding-left: 25px !important;
								}

								.p {
									font-size: 16px !important;
									line-height: 28px !important;
									padding-left: 25px !important;
									padding-right: 25px !important;
									padding-bottom: 25px !important;
								}

								.CTA_wrap {
									padding-left: 25px !important;
									padding-right: 25px !important;
									padding-bottom: 25px !important;
								}

								.footer {
									padding-left: 0px !important;
									padding-right:0px !important;
								}

								.number_wrap {
									padding-left: 25px !important;
									padding-right: 25px !important;
								}

								.unsubscribe {
									padding-left: 25px !important;
									padding-right: 25px !important;
								}
							}
						</style>
					</head>
					<body style=""padding:0; margin:0; -webkit-text-size-adjust:none; -ms-text-size-adjust:100%; background-color:#e8e8e8; font-family: 'Montserrat',Arial, Helvetica, sans-serif; font-size:16px; line-height:24px; color:#05202D"">
						<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""table-layout:fixed;"">
							<tbody>
								<tr>
									<td bgcolor=""#EBEBEB"" style=""font-size:0px"">&zwnj;</td>
										<td align=""center"" width=""600"">
											<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""margin-top: 1vh;background-color: #FFFFFF;"">
												<tbody style=""text-align: justify;"">
													<tr>
														<td>
															<img src=""http://dotnetfin.com/img/logo_64x64.png"" style=""padding-top:30px; padding-right: 50px; padding-left: 50px;padding-bottom: 30px;"" alt=""logo"" class=""logo"" align=""left""/>
														</td>
													</tr>");

        public void WithH1Header(string header)
        {
            builder.Append(@$"
            <tr>
				<td class=""h1"" align=""left"" style=""padding-top:20px; padding-right: 50px; padding-left: 50px;padding-bottom: 0px; font-size: 36px; line-height: 43px; font-weight: 700; color:#003580;"">
					{header}
                </td>
            </tr>");
        }

        public void WithH2Header(string header)
        {
            builder.Append(@$"
            <tr>
				<td class=""h2"" align=""left"" style=""padding-top:25px; padding-right: 50px; padding-left: 50px;padding-bottom: 0px; font-size: 18px; line-height: 27px; font-weight:600; color:#003580;"">
					{header}
                </td>
            </tr>");
        }

        public void WithParagraph(string paragraph)
        {
            builder.Append(@$"
            <tr><td class=""p"" style=""padding-top:25px; padding-right: 50px; padding-left: 50px;padding-bottom: 0px; font-size: 16px; line-height: 26px; font-weight:200; color:#05202D; opacity: 0.7;"">{paragraph}</td></tr>");
        }


        public string Build()
        {
            builder.Append(@"
										</tbody>
									</table>
								</td>
							<td bgcolor=""#EBEBEB"" style=""font-size:0px"">&zwnj;</td>
						</tr>
					</tbody>
                </table>
                </body>
            </html>");
            return builder.ToString();
        }
    }
}
