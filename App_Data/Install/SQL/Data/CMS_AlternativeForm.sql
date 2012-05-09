SET IDENTITY_INSERT [CMS_AlternativeForm] ON
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (124, N'Registration form', N'RegistrationForm', 59, N'<form><field column="UserID" visible="false" /><field column="UserName" fieldcaption="User name"><settings><controlname>username</controlname></settings></field><field column="FirstName" fieldcaption="First name" minstringlength="1" validationerrormessage="Please enter some first name." /><field column="MiddleName" visible="false" /><field column="LastName" fieldcaption="Last name" minstringlength="1" validationerrormessage="Please enter some last name." /><field column="FullName" fieldcaption="Full name" visible="false" allowempty="false" /><field column="Email" minstringlength="5" validationerrormessage="Please enter some email."><settings><controlname>emailinput</controlname></settings></field><field column="UserPassword" fieldcaption="Password" minstringlength="2"><settings><controlname>passwordconfirmator</controlname><showstrength>True</showstrength></settings></field><field column="PreferredCultureCode" visible="false" /><field column="PreferredUICultureCode" visible="false" /><field column="UserEnabled" visible="false" defaultvalue="false" /><field column="UserIsEditor" visible="false" defaultvalue="false" /><field column="UserIsGlobalAdministrator" visible="false" defaultvalue="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" /><field column="UserPasswordFormat" visible="false" /><field column="UserCreated" visible="false"><settings><editTime>false</editTime></settings></field><field column="LastLogon" visible="false"><settings><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" /><field column="UserGUID" visible="false" /><field column="UserLastModified" visible="false"><settings><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" /><field column="UserVisibility" visible="false" /><field column="UserSettingsID" visible="false" /><field column="UserNickName" visible="false" /><field column="UserPicture" visible="false" /><field column="UserSignature" visible="false" /><field column="UserURLReferrer" visible="false" /><field column="UserCampaign" visible="false" /><field column="UserMessagingNotificationEmail" visible="false" /><field column="UserCustomData" visible="false" /><field column="UserRegistrationInfo" visible="false" /><field column="UserPreferences" visible="false" /><field column="UserActivationDate" visible="false" /><field column="UserActivatedByUserID" visible="false" /><field column="UserTimeZoneID" fieldcaption="Time zone" visible="false"><settings><controlname>timezoneselector</controlname></settings></field><field column="UserAvatarID" fieldcaption="Avatar" visible="false"><settings><controlname>useravatarselector</controlname></settings></field><field column="UserBadgeID" visible="false" /><field column="UserShowSplashScreen" visible="false" /><field column="UserActivityPoints" visible="false" /><field column="UserForumPosts" visible="false" /><field column="UserBlogComments" visible="false" /><field column="UserGender" fieldcaption="Gender"><settings><controlname>radiobuttonscontrol</controlname><repeatdirection>horizontal</repeatdirection><options>&lt;item value="1" text="{$general.male$}" /&gt;&lt;item value="2" text="{$general.female$}" /&gt;</options></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth" visible="false"><settings><EditTime>false</EditTime></settings></field><field column="UserMessageBoardPosts" visible="false" /><field column="UserSettingsUserGUID" visible="false" /><field column="UserSettingsUserID" visible="false" /><field column="UserBlogPosts" visible="false" /></form>', N'<table class="CustomRegistrationForm">
	<tbody>
		<tr>
			<td>
				$$label:UserName$$</td>
			<td>
				$$input:UserName$$$$validation:UserName$$</td>
		</tr>
		<tr>
			<td>
				$$label:FirstName$$</td>
			<td>
				$$input:FirstName$$$$validation:FirstName$$</td>
		</tr>
		<tr>
			<td>
				$$label:LastName$$</td>
			<td>
				$$input:LastName$$$$validation:LastName$$</td>
		</tr>
		<tr>
			<td>
				$$label:Email$$</td>
			<td>
				$$input:Email$$$$validation:Email$$</td>
		</tr>
		<tr>
			<td style="vertical-align: top; padding-top: 6px;" valign="top">
				<span class="EditingFormLabel">$$label:UserPassword$$</span>
				<div style="margin-top: 44px;">
					<span class="EditingFormLabel">Confirm password:</span></div>
			</td>
			<td>
				$$input:UserPassword$$$$validation:UserPassword$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserGender$$</td>
			<td>
				$$input:UserGender$$$$validation:UserGender$$</td>
		</tr>
	</tbody>
</table>', '005a6c1c-a442-4229-ba5c-80f423dea704', '20111021 17:17:12', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (125, N'Display profile', N'DisplayProfile', 59, N'<form><field column="UserID" visible="false" dependsonanotherfield="false" /><field column="UserName" fieldcaption="Name" dependsonanotherfield="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" visible="false" dependsonanotherfield="false" /><field column="MiddleName" visible="false" dependsonanotherfield="false" /><field column="LastName" visible="false" dependsonanotherfield="false" /><field column="FullName" fieldcaption="Full name" dependsonanotherfield="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="Email" visibility="authenticated" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname><FilterMode /><FilterEnabled /></settings></field><field column="UserPassword" visible="false" dependsonanotherfield="false" /><field column="PreferredCultureCode" visible="false" dependsonanotherfield="false" /><field column="PreferredUICultureCode" visible="false" dependsonanotherfield="false" /><field column="UserEnabled" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsEditor" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsGlobalAdministrator" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserPasswordFormat" visible="false" dependsonanotherfield="false" /><field column="UserCreated" fieldcaption="Created" dependsonanotherfield="false"><settings><controlname>viewdate</controlname></settings></field><field column="LastLogon" visible="false" dependsonanotherfield="false"><settings><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" dependsonanotherfield="false" /><field column="UserGUID" visible="false" dependsonanotherfield="false" /><field column="UserLastModified" visible="false" dependsonanotherfield="false"><settings><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" dependsonanotherfield="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserVisibility" visible="false" dependsonanotherfield="false" /><field column="UserIsDomain" dependsonanotherfield="false" /><field column="UserHasAllowedCultures" dependsonanotherfield="false" /><field column="UserSiteManagerDisabled" dependsonanotherfield="false" /><field column="UserSettingsID" visible="false" dependsonanotherfield="false" /><field column="UserNickName" dependsonanotherfield="false" /><field column="UserPicture" visible="false" dependsonanotherfield="false" /><field column="UserSignature" visible="false" dependsonanotherfield="false" /><field column="UserURLReferrer" visible="false" dependsonanotherfield="false" /><field column="UserCampaign" visible="false" dependsonanotherfield="false" /><field column="UserMessagingNotificationEmail" visible="false" dependsonanotherfield="false" /><field column="UserCustomData" visible="false" dependsonanotherfield="false" /><field column="UserRegistrationInfo" visible="false" dependsonanotherfield="false" /><field column="UserPreferences" visible="false" dependsonanotherfield="false" /><field column="UserActivationDate" visible="false" dependsonanotherfield="false"><settings><controlname>viewdate</controlname></settings></field><field column="UserActivatedByUserID" visible="false" dependsonanotherfield="false" /><field column="UserTimeZoneID" visible="false" dependsonanotherfield="false" /><field column="UserAvatarID" dependsonanotherfield="false"><settings><controlname>viewuseravatar</controlname></settings></field><field column="UserBadgeID" fieldcaption="Badge" dependsonanotherfield="false"><settings><controlname>viewbadgeimage</controlname></settings></field><field column="UserShowSplashScreen" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserActivityPoints" fieldcaption="Community points" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserForumPosts" fieldcaption="Forum posts" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserBlogComments" fieldcaption="Blog comments" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserGender" fieldcaption="Gender" dependsonanotherfield="false"><settings><controlname>viewusergender</controlname></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth" dependsonanotherfield="false" visibility=""><settings><controlname>viewdate</controlname><DisplayNow /><TimeZoneType /><EditTime /></settings></field><field column="UserMessageBoardPosts" fieldcaption="Message board posts" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserSettingsUserGUID" visible="false" dependsonanotherfield="false" /><field column="UserSettingsUserID" visible="false" dependsonanotherfield="false" /><field column="WindowsLiveID" dependsonanotherfield="false" /><field column="UserBlogPosts" fieldcaption="Blog posts" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserWaitingForApproval" dependsonanotherfield="false" /><field column="UserDialogsConfiguration" dependsonanotherfield="false" /><field column="UserDescription" dependsonanotherfield="false" /><field column="UserUsedWebParts" dependsonanotherfield="false" /><field column="UserUsedWidgets" dependsonanotherfield="false" /><field column="UserFacebookID" dependsonanotherfield="false" /><field column="UserAuthenticationGUID" dependsonanotherfield="false" /><field column="UserSkype" dependsonanotherfield="false" /><field column="UserIM" dependsonanotherfield="false" /><field column="UserPhone" dependsonanotherfield="false" /><field column="UserPosition" dependsonanotherfield="false" /><field column="UserBounces" dependsonanotherfield="false" /></form>', N'<table cellpadding="2">
    <tbody>
        <tr>
            <td rowspan="8" colspan="2">$$input:UserAvatarID$$</td>
            <td colspan="2">
            <div style="font-size: 20px; font-weight: bold;">$$input:UserName$$</div>
            </td>
        </tr>
        <tr>
            <td><strong>$$label:UserBadgeID$$</strong></td>
            <td>$$input:UserBadgeID$$</td>
        </tr>
        <tr>
            <td><strong>$$label:FullName$$</strong></td>
            <td>$$input:FullName$$</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:Email$$</strong></td>
            <td>$$input:Email$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserCreated$$</strong></td>
            <td>$$input:UserCreated$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserGender$$</strong></td>
            <td>$$input:UserGender$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserDateOfBirth$$</strong></td>
            <td>$$input:UserDateOfBirth$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserForumPosts$$</strong></td>
            <td>$$input:UserForumPosts$$</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserMessageBoardPosts$$</strong></td>
            <td>$$input:UserMessageBoardPosts$$</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserBlogPosts$$</strong></td>
            <td>$$input:UserBlogPosts$$</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserBlogComments$$</strong></td>
            <td>$$input:UserBlogComments$$</td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserActivityPoints$$</strong></td>
            <td>$$input:UserActivityPoints$$</td>
            <td colspan="2">&nbsp;</td>
        </tr>
    </tbody>
</table>', '58c69789-2f4b-4e9b-b557-cabf89c65064', '20110225 12:34:20', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (126, N'Display profile', N'DisplayProfile', 1748, N'<form><field column="GroupID" visible="false" /><field column="GroupGUID" visible="false" /><field column="GroupLastModified" visible="false"><settings><editTime>false</editTime></settings></field><field column="GroupSiteID" visible="false" /><field column="GroupDisplayName" fieldcaption="Group display name" fieldtype="usercontrol"><settings><controlname>viewsecuretext</controlname></settings></field><field column="GroupName" visible="false" /><field column="GroupDescription" fieldcaption="Description" fieldtype="usercontrol"><settings><controlname>viewsecuretext</controlname></settings></field><field column="GroupNodeGUID" visible="false" /><field column="GroupApproveMembers" visible="false" /><field column="GroupAccess" fieldcaption="Access" fieldtype="usercontrol"><settings><controlname>viewgroupaccess</controlname></settings></field><field column="GroupCreatedByUserID" visible="false" /><field column="GroupEnabled" visible="false" defaultvalue="false" /><field column="GroupApprovedByUserID" visible="false" /><field column="GroupAvatarID" fieldcaption="Group avatar ID" fieldtype="usercontrol"><settings><controlname>viewgroupavatar</controlname></settings></field><field column="GroupApproved" visible="false" /><field column="GroupCreatedWhen" fieldcaption="Created" fieldtype="usercontrol"><settings><controlname>viewdate</controlname></settings></field></form>', N'<table>
    <tbody>
        <tr>
            <td rowspan="4">$$input:GroupAvatarID$$</td>
            <td style="vertical-align: top;" colspan="3">
            <div style="font-size: 20px; font-weight: bold;">$$input:GroupDisplayName$$</div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="2">$$input:GroupDescription$$</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><strong>$$label:GroupAccess$$</strong></td>
            <td style="width: 100%;">$$input:GroupAccess$$</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><strong>$$label:GroupCreatedWhen$$</strong></td>
            <td style="width: 100%;">$$input:GroupCreatedWhen$$</td>
        </tr>
    </tbody>
</table>', '1cd04c91-eee1-49c3-b7a8-385a5f521ea5', '20081210 14:50:06', NULL)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (128, N'Edit profile', N'EditProfile', 59, N'<form><field column="UserID" visible="false" dependsonanotherfield="false" /><field column="UserName" fieldcaption="Username" dependsonanotherfield="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" visible="false" dependsonanotherfield="false" /><field column="MiddleName" visible="false" dependsonanotherfield="false" /><field column="LastName" visible="false" dependsonanotherfield="false" /><field column="FullName" fieldcaption="Full name" allowempty="false" dependsonanotherfield="false" /><field column="Email" visibility="authenticated" dependsonanotherfield="false"><settings><controlname>emailinput</controlname></settings></field><field column="UserPassword" visible="false" dependsonanotherfield="false" /><field column="PreferredCultureCode" visible="false" dependsonanotherfield="false" /><field column="PreferredUICultureCode" visible="false" dependsonanotherfield="false" /><field column="UserEnabled" visible="false" dependsonanotherfield="false" /><field column="UserIsEditor" visible="false" dependsonanotherfield="false" /><field column="UserIsGlobalAdministrator" visible="false" dependsonanotherfield="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserPasswordFormat" visible="false" dependsonanotherfield="false" /><field column="UserCreated" fieldcaption="User created" visible="false" dependsonanotherfield="false"><settings><controlname>viewdate</controlname></settings></field><field column="LastLogon" visible="false" dependsonanotherfield="false"><settings><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" dependsonanotherfield="false" /><field column="UserGUID" visible="false" dependsonanotherfield="false" /><field column="UserLastModified" visible="false" dependsonanotherfield="false"><settings><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" dependsonanotherfield="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserVisibility" visible="false" dependsonanotherfield="false" /><field column="UserIsDomain" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserHasAllowedCultures" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserSiteManagerDisabled" dependsonanotherfield="false" /><field column="UserSettingsID" visible="false" dependsonanotherfield="false" /><field column="UserNickName" fieldcaption="Nickname" dependsonanotherfield="false" /><field column="UserPicture" visible="false" dependsonanotherfield="false" /><field column="UserSignature" fieldcaption="Signature" dependsonanotherfield="false"><settings><rows>5</rows><cols>33</cols></settings></field><field column="UserURLReferrer" visible="false" dependsonanotherfield="false" /><field column="UserCampaign" visible="false" dependsonanotherfield="false" /><field column="UserMessagingNotificationEmail" fieldcaption="Messaging notification e-mail" dependsonanotherfield="false"><settings><controlname>emailinput</controlname></settings></field><field column="UserCustomData" visible="false" dependsonanotherfield="false" /><field column="UserRegistrationInfo" visible="false" dependsonanotherfield="false" /><field column="UserPreferences" visible="false" dependsonanotherfield="false" /><field column="UserActivationDate" visible="false" dependsonanotherfield="false" /><field column="UserActivatedByUserID" visible="false" dependsonanotherfield="false" /><field column="UserTimeZoneID" fieldcaption="Time zone" fielddescription="Enables user to select his timezone." dependsonanotherfield="false"><settings><controlname>timezoneselector</controlname></settings></field><field column="UserAvatarID" fieldcaption="Avatar" dependsonanotherfield="false"><settings><controlname>useravatarselector</controlname></settings></field><field column="UserBadgeID" visible="false" dependsonanotherfield="false" /><field column="UserShowSplashScreen" visible="false" dependsonanotherfield="false" /><field column="UserActivityPoints" visible="false" dependsonanotherfield="false" /><field column="UserForumPosts" visible="false" dependsonanotherfield="false" /><field column="UserBlogComments" visible="false" dependsonanotherfield="false" /><field column="UserGender" fieldcaption="Gender" dependsonanotherfield="false"><settings><controlname>radiobuttonscontrol</controlname><repeatdirection>horizontal</repeatdirection><options><item value="1" text="{$General.Male$}" /><item value="2" text="{$General.Female$}" /></options></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth" dependsonanotherfield="false"><settings><DisplayNow>false</DisplayNow><EditTime>false</EditTime></settings></field><field column="UserMessageBoardPosts" visible="false" dependsonanotherfield="false" /><field column="UserSettingsUserGUID" visible="false" dependsonanotherfield="false" /><field column="UserSettingsUserID" visible="false" dependsonanotherfield="false" /><field column="WindowsLiveID" dependsonanotherfield="false" /><field column="UserBlogPosts" visible="false" dependsonanotherfield="false" /><field column="UserWaitingForApproval" visible="false" dependsonanotherfield="false" /><field column="UserDialogsConfiguration" visible="false" dependsonanotherfield="false" /><field column="UserDescription" visible="false" dependsonanotherfield="false" /><field column="UserUsedWebParts" dependsonanotherfield="false" /><field column="UserUsedWidgets" dependsonanotherfield="false" /><field column="UserFacebookID" dependsonanotherfield="false" /><field column="UserAuthenticationGUID" dependsonanotherfield="false" /><field column="UserSkype" dependsonanotherfield="false" /><field column="UserIM" dependsonanotherfield="false" /><field column="UserPhone" dependsonanotherfield="false" /><field column="UserPosition" dependsonanotherfield="false" /><field column="UserBounces" visible="false" dependsonanotherfield="false" /></form>', N'', 'ae8d424c-7eed-4b91-a555-f87c0df05597', '20110225 10:08:10', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (129, N'Edit profile (MyDesk)', N'EditProfileMyDesk', 59, N'<form><field column="UserID" visible="false" /><field column="UserName" fieldcaption="{$general.username$}"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" fieldcaption="{$general.firstname$}" /><field column="MiddleName" visible="false" /><field column="LastName" fieldcaption="{$general.lastname$}" /><field column="FullName" fieldcaption="{$general.fullname$}" allowempty="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="Email" fieldcaption="{$general.email$}"><settings><controlname>emailinput</controlname></settings></field><field column="UserPassword" visible="false" /><field column="PreferredCultureCode" fieldcaption="{$MyDesk.MyProfile.Culture$}"><settings><controlname>sitecultureselector</controlname></settings></field><field column="PreferredUICultureCode" fieldcaption="{$MyDesk.MyProfile.UICulture$}"><settings><controlname>uicultureselector</controlname></settings></field><field column="UserEnabled" visible="false" /><field column="UserIsEditor" visible="false" /><field column="UserIsGlobalAdministrator" visible="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" /><field column="UserPasswordFormat" visible="false" /><field column="UserCreated" visible="false"><settings><editTime>false</editTime></settings></field><field column="LastLogon" visible="false"><settings><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" /><field column="UserGUID" visible="false" /><field column="UserLastModified" visible="false"><settings><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" /><field column="UserSettingsID" visible="false" /><field column="UserNickName" fieldcaption="{$general.nickname$}" /><field column="UserPicture" visible="false" /><field column="UserSignature" fieldcaption="{$general.signature$}" controlcssclass="TextAreaField"><settings><size>200</size><rows>5</rows><cols>33</cols></settings></field><field column="UserURLReferrer" visible="false" /><field column="UserCampaign" visible="false" /><field column="UserMessagingNotificationEmail" fieldcaption="{$Messaging.NotificationEmail$}"><settings><controlname>emailinput</controlname></settings></field><field column="UserCustomData" visible="false" /><field column="UserRegistrationInfo" visible="false" /><field column="UserPreferences" visible="false" /><field column="UserActivationDate" visible="false" /><field column="UserActivatedByUserID" visible="false" /><field column="UserTimeZoneID" fieldcaption="{$general.timezone$}"><settings><controlname>timezoneselector</controlname></settings></field><field column="UserAvatarID" fieldcaption="{$general.avatar$}"><settings><controlname>useravatarselector</controlname></settings></field><field column="UserBadgeID" visible="false" /><field column="UserShowSplashScreen" fieldcaption="{$adm.user.lblusershowsplashscreen$}" defaultvalue="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserActivityPoints" visible="false" /><field column="UserForumPosts" visible="false" /><field column="UserBlogComments" visible="false" /><field column="UserGender" fieldcaption="{$general.gender$}"><settings><controlname>radiobuttonscontrol</controlname><RepeatDirection>horizontal</RepeatDirection><Options>1;{$General.Male$}
2;{$General.Female$}</Options></settings></field><field column="UserDateOfBirth" fieldcaption="{$general.DateOfBirth$}"><settings><EditTime>False</EditTime></settings></field><field column="UserMessageBoardPosts" visible="false" /><field column="UserSettingsUserGUID" visible="false" /><field column="UserSettingsUserID" visible="false" /><field column="UserBlogPosts" visible="false" /><field column="UserWaitingForApproval" visible="false" /><field column="UserSkype" fieldcaption="Skype account" visible="true" /><field column="UserIM" fieldcaption="IM" visible="true" /><field column="UserPhone" fieldcaption="Phone number" visible="true" /></form>', N'<table cellspacing="2" cellpadding="2">
    <tbody>
        <tr>
            <td>$$label:UserName$$</td>
            <td>$$input:UserName$$ $$validation:UserName$$</td>
        </tr>
        <tr>
            <td>$$label:FullName$$</td>
            <td>$$input:FullName$$ $$validation:FullName$$</td>
        </tr>
        <tr>
            <td>$$label:FirstName$$</td>
            <td>$$input:FirstName$$ $$validation:FirstName$$</td>
        </tr>
        <tr>
            <td>$$label:LastName$$</td>
            <td>$$input:LastName$$ $$validation:LastName$$</td>
        </tr>
        <tr>
            <td>$$label:UserNickName$$</td>
            <td>$$input:UserNickName$$ $$validation:UserNickName$$</td>
        </tr>
        <tr>
            <td>$$label:Email$$</td>
            <td>$$input:Email$$ $$validation:Email$$</td>
        </tr>
        <tr>
            <td>$$label:PreferredCultureCode$$</td>
            <td>$$input:PreferredCultureCode$$ $$validation:PreferredCultureCode$$</td>
        </tr>
        <tr>
            <td>$$label:PreferredUICultureCode$$</td>
            <td>$$input:PreferredUICultureCode$$ $$validation:PreferredUICultureCode$$</td>
        </tr>
        <tr>
            <td>$$label:UserMessagingNotificationEmail$$</td>
            <td>$$input:UserMessagingNotificationEmail$$ $$validation:UserMessagingNotificationEmail$$</td>
        </tr>
        <tr>
            <td>$$label:UserTimeZoneID$$</td>
            <td>$$input:UserTimeZoneID$$$$validation:UserTimeZoneID$$</td>
        </tr>
        <tr>
            <td>$$label:UserSignature$$</td>
            <td>$$input:UserSignature$$ $$validation:UserSignature$$</td>
        </tr>
        <tr>
            <td>$$label:UserGender$$</td>
            <td>$$input:UserGender$$ $$validation:UserGender$$</td>
        </tr>
        <tr>
            <td>$$label:UserDateOfBirth$$</td>
            <td>$$input:UserDateOfBirth$$ $$validation:UserDateOfBirth$$</td>
        </tr>
        <tr>
            <td>$$label:UserPhone$$</td>
            <td>$$input:UserPhone$$ $$validation:UserPhone$$</td>
        </tr>
        <tr>
            <td>$$label:UserSkype$$</td>
            <td>$$input:UserSkype$$ $$validation:UserSkype$$</td>
        </tr>
        <tr>
            <td>$$label:UserIM$$</td>
            <td>$$input:UserIM$$ $$validation:UserIM$$</td>
        </tr>
        <tr>
            <td>$$label:UserAvatarID$$</td>
            <td>$$input:UserAvatarID$$ $$validation:UserAvatarID$$</td>
        </tr>
        <tr>
            <td>$$label:UserShowSplashScreen$$</td>
            <td>$$input:UserShowSplashScreen$$ $$validation:UserShowSplashScreen$$</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>$$submitbutton$$</td>
        </tr>
    </tbody>
</table>', '8815b022-8e63-4f96-9710-a901e35e04d2', '20120102 13:45:39', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (150, N'Edit profile (Community)', N'EditProfileCommunity', 59, N'<form><field column="UserID" visible="false" /><field column="UserName" fieldcaption="Username"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" visible="false" /><field column="MiddleName" visible="false" /><field column="LastName" visible="false" /><field column="FullName" fieldcaption="Full name" allowempty="false" /><field column="Email" allowusertochangevisibility="true" visibility="authenticated" visibilitycontrol="RadioButtonsHorizontalVisibilityControl"><settings><controlname>emailinput</controlname></settings></field><field column="UserPassword" visible="false" /><field column="PreferredCultureCode" visible="false" /><field column="PreferredUICultureCode" visible="false" /><field column="UserEnabled" visible="false" /><field column="UserIsEditor" visible="false" /><field column="UserIsGlobalAdministrator" visible="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" /><field column="UserPasswordFormat" visible="false" /><field column="UserCreated" visible="false"><settings><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="LastLogon" visible="false"><settings><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" /><field column="UserGUID" visible="false" /><field column="UserLastModified" visible="false"><settings><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" /><field column="UserVisibility"><settings><controlname>largetextarea</controlname></settings></field><field column="UserSettingsID" visible="false" /><field column="UserNickName" fieldcaption="Nickname" /><field column="UserPicture" visible="false" /><field column="UserSignature" fieldcaption="Signature" controlcssclass="SignatureField"><settings><rows>5</rows><cols>33</cols></settings></field><field column="UserURLReferrer" visible="false" /><field column="UserCampaign" visible="false" /><field column="UserMessagingNotificationEmail" fieldcaption="Messaging notification e-mail"><settings><controlname>emailinput</controlname></settings></field><field column="UserCustomData" visible="false" /><field column="UserRegistrationInfo" visible="false" /><field column="UserPreferences" visible="false" /><field column="UserActivationDate" visible="false"><settings><timezonetype>inherit</timezonetype></settings></field><field column="UserActivatedByUserID" visible="false" /><field column="UserTimeZoneID" fieldcaption="Time zone" fielddescription="Enables user to select his timezone."><settings><controlname>timezoneselector</controlname></settings></field><field column="UserAvatarID" fieldcaption="Avatar"><settings><controlname>useravatarselector</controlname></settings></field><field column="UserBadgeID" visible="false" /><field column="UserShowSplashScreen" visible="false" /><field column="UserActivityPoints" visible="false" /><field column="UserForumPosts" visible="false" /><field column="UserBlogComments" visible="false" /><field column="UserGender" fieldcaption="Gender"><settings><controlname>radiobuttonscontrol</controlname><RepeatDirection>horizontal</RepeatDirection><Options><item value="1" text="{$General.Male$}" /><item value="2" text="{$General.Female$}" /></Options></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth"><settings><EditTime>False</EditTime></settings></field><field column="UserMessageBoardPosts" visible="false" /><field column="UserSettingsUserGUID" visible="false" /><field column="UserSettingsUserID" visible="false" /><field column="UserBlogPosts" visible="false" /><field column="UserWaitingForApproval" visible="false" /><field column="UserSkype" visible="true" /><field column="UserIM" visible="true" /><field column="UserPhone" visible="true" /><field column="UserPosition" visible="true" /></form>', N'<table>
	<tbody>
		<tr>
			<td>
				$$label:UserName$$</td>
			<td>
				$$input:UserName$$ $$validation:UserName$$</td>
		</tr>
		<tr>
			<td>
				$$label:FullName$$</td>
			<td>
				$$input:FullName$$ $$validation:FullName$$</td>
		</tr>
		<tr>
			<td>
				$$label:Email$$</td>
			<td>
				$$input:Email$$ $$validation:Email$$</td>
		</tr>
		<tr>
			<td>
				Display my e-mail to</td>
			<td>
				$$visibility:Email$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserNickName$$</td>
			<td>
				$$input:UserNickName$$ $$validation:UserNickName$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserSignature$$</td>
			<td>
				$$input:UserSignature$$ $$validation:UserSignature$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserMessagingNotificationEmail$$</td>
			<td>
				$$input:UserMessagingNotificationEmail$$ $$validation:UserMessagingNotificationEmail$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserTimeZoneID$$</td>
			<td>
				$$input:UserTimeZoneID$$ $$validation:UserTimeZoneID$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserAvatarID$$</td>
			<td>
				$$input:UserAvatarID$$ $$validation:UserAvatarID$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserGender$$</td>
			<td>
				$$input:UserGender$$ $$validation:UserGender$$</td>
		</tr>
		<tr>
			<td>
				$$label:UserDateOfBirth$$</td>
			<td>
				$$input:UserDateOfBirth$$ $$validation:UserDateOfBirth$$</td>
		</tr>
		<tr>
			<td></td>
			<td>
				$$submitbutton$$</td>
		</tr>
	</tbody>
</table>', '52ab7093-1c5a-4af7-b085-b223f67ab909', '20110908 13:36:49', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (890, N'Edit profile (Intranet)', N'EditProfileIntranet', 59, N'<form><field column="UserID" visible="false" dependsonanotherfield="false" /><field column="UserName" fieldcaption="User name" dependsonanotherfield="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" visible="false" dependsonanotherfield="false" /><field column="MiddleName" visible="false" dependsonanotherfield="false" /><field column="LastName" visible="false" dependsonanotherfield="false" /><field column="FullName" fieldcaption="Full name" dependsonanotherfield="false"><settings><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="Email" allowusertochangevisibility="true" visibilitycontrol="RadioButtonsHorizontalVisibilityControl" dependsonanotherfield="false" visibility=""><settings><controlname>emailinput</controlname><FilterMode /><FilterEnabled /></settings></field><field column="UserPassword" visible="false" dependsonanotherfield="false" /><field column="PreferredCultureCode" visible="false" dependsonanotherfield="false" /><field column="PreferredUICultureCode" visible="false" dependsonanotherfield="false" /><field column="UserEnabled" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsEditor" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsGlobalAdministrator" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserPasswordFormat" visible="false" dependsonanotherfield="false" /><field column="UserCreated" visible="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="LastLogon" visible="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" dependsonanotherfield="false" /><field column="UserGUID" visible="false" dependsonanotherfield="false" /><field column="UserLastModified" visible="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" dependsonanotherfield="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserVisibility" fieldcaption="Display my e-mail to" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserIsDomain" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserHasAllowedCultures" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserSiteManagerDisabled" dependsonanotherfield="false" /><field column="UserSettingsID" visible="false" dependsonanotherfield="false" /><field column="UserNickName" fieldcaption="Nickname" dependsonanotherfield="false" /><field column="UserPicture" visible="false" dependsonanotherfield="false" /><field column="UserSignature" fieldcaption="Signature" dependsonanotherfield="false" /><field column="UserURLReferrer" visible="false" dependsonanotherfield="false" /><field column="UserCampaign" visible="false" dependsonanotherfield="false" /><field column="UserMessagingNotificationEmail" fieldcaption="Messaging notification e-mail" dependsonanotherfield="false"><settings><controlname>emailinput</controlname></settings></field><field column="UserCustomData" visible="false" dependsonanotherfield="false" /><field column="UserRegistrationInfo" visible="false" dependsonanotherfield="false" /><field column="UserPreferences" visible="false" dependsonanotherfield="false" /><field column="UserActivationDate" visible="false" dependsonanotherfield="false"><settings><timezonetype>inherit</timezonetype></settings></field><field column="UserActivatedByUserID" visible="false" dependsonanotherfield="false" /><field column="UserTimeZoneID" fieldcaption="Time zone" dependsonanotherfield="false"><settings><controlname>timezoneselector</controlname></settings></field><field column="UserAvatarID" fieldcaption="Avatar" dependsonanotherfield="false"><settings><controlname>useravatarselector</controlname></settings></field><field column="UserBadgeID" visible="false" dependsonanotherfield="false" /><field column="UserShowSplashScreen" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserActivityPoints" visible="false" dependsonanotherfield="false" /><field column="UserForumPosts" visible="false" dependsonanotherfield="false" /><field column="UserBlogComments" visible="false" dependsonanotherfield="false" /><field column="UserGender" fieldcaption="Gender" dependsonanotherfield="false"><settings><controlname>radiobuttonscontrol</controlname><RepeatDirection>vertical</RepeatDirection><Options><item value="1" text="{$General.Male$}" /><item value="2" text="{$General.Female$}" /></Options></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth" dependsonanotherfield="false"><settings><EditTime>False</EditTime></settings></field><field column="UserMessageBoardPosts" visible="false" dependsonanotherfield="false" /><field column="UserSettingsUserGUID" visible="false" dependsonanotherfield="false" /><field column="UserSettingsUserID" visible="false" dependsonanotherfield="false" /><field column="WindowsLiveID" dependsonanotherfield="false" /><field column="UserBlogPosts" visible="false" dependsonanotherfield="false" /><field column="UserWaitingForApproval" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserDialogsConfiguration" visible="false" dependsonanotherfield="false" /><field column="UserDescription" visible="false" dependsonanotherfield="false" /><field column="UserUsedWebParts" dependsonanotherfield="false" /><field column="UserUsedWidgets" dependsonanotherfield="false" /><field column="UserFacebookID" dependsonanotherfield="false" /><field column="UserAuthenticationGUID" dependsonanotherfield="false" /><field column="UserSkype" fieldcaption="Skype account" visible="true" dependsonanotherfield="false" visibility="" /><field column="UserIM" fieldcaption="Instant messenger" visible="true" dependsonanotherfield="false" visibility="" /><field column="UserPhone" fieldcaption="Phone number" visible="true" dependsonanotherfield="false" visibility="" /><field column="UserPosition" dependsonanotherfield="false" /><field column="UserBounces" visible="false" dependsonanotherfield="false" /></form>', N'<table>
    <tbody>
        <tr>
            <td>$$label:UserName$$</td>
            <td>$$input:UserName$$ $$validation:UserName$$</td>
        </tr>
        <tr>
            <td>$$label:FullName$$</td>
            <td>$$input:FullName$$ $$validation:FullName$$</td>
        </tr>
        <tr>
            <td>$$label:Email$$</td>
            <td>$$input:Email$$ $$validation:Email$$</td>
        </tr>
        <tr>
            <td>$$label:UserVisibility$$</td>
            <td>$$visibility:Email$$</td>
        </tr>
        <tr>
            <td>$$label:UserNickName$$</td>
            <td>$$input:UserNickName$$ $$validation:UserNickName$$</td>
        </tr>
        <tr>
            <td>$$label:UserSignature$$</td>
            <td>$$input:UserSignature$$ $$validation:UserSignature$$</td>
        </tr>
        <tr>
            <td>$$label:UserMessagingNotificationEmail$$</td>
            <td>$$input:UserMessagingNotificationEmail$$ $$validation:UserMessagingNotificationEmail$$</td>
        </tr>
        <tr>
            <td>$$label:UserTimeZoneID$$</td>
            <td>$$input:UserTimeZoneID$$ $$validation:UserTimeZoneID$$</td>
        </tr>
        <tr>
            <td>$$label:UserAvatarID$$</td>
            <td>$$input:UserAvatarID$$ $$validation:UserAvatarID$$</td>
        </tr>
        <tr>
            <td>$$label:UserGender$$</td>
            <td>$$input:UserGender$$ $$validation:UserGender$$</td>
        </tr>
        <tr>
            <td>$$label:UserDateOfBirth$$</td>
            <td>$$input:UserDateOfBirth$$ $$validation:UserDateOfBirth$$</td>
        </tr>
        <tr>
            <td>$$label:UserSkype$$</td>
            <td>$$input:UserSkype$$ $$validation:UserSkype$$</td>
        </tr>
        <tr>
            <td>$$label:UserIM$$</td>
            <td>$$input:UserIM$$ $$validation:UserIM$$</td>
        </tr>
        <tr>
            <td>$$label:UserPhone$$</td>
            <td>$$input:UserPhone$$ $$validation:UserPhone$$</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>$$submitbutton$$</td>
        </tr>
    </tbody>
</table>', '38f17d82-fe29-411e-9711-52e04236c7fe', '20110225 12:54:00', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (891, N'Display profile (Intranet)', N'DisplayProfileIntranet', 59, N'<form><field column="UserID" visible="false" dependsonanotherfield="false" /><field column="UserName" fieldcaption="Name" dependsonanotherfield="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" visible="false" dependsonanotherfield="false" /><field column="MiddleName" visible="false" dependsonanotherfield="false" /><field column="LastName" visible="false" dependsonanotherfield="false" /><field column="FullName" fieldcaption="Full name" dependsonanotherfield="false"><settings><controlname>viewsecuretext</controlname></settings></field><field column="Email" visibility="authenticated" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname><FilterMode /><FilterEnabled /></settings></field><field column="UserPassword" visible="false" dependsonanotherfield="false" /><field column="PreferredCultureCode" visible="false" dependsonanotherfield="false" /><field column="PreferredUICultureCode" visible="false" dependsonanotherfield="false" /><field column="UserEnabled" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsEditor" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsGlobalAdministrator" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserPasswordFormat" visible="false" dependsonanotherfield="false" /><field column="UserCreated" fieldcaption="Created" dependsonanotherfield="false"><settings><timezonetype>inherit</timezonetype><controlname>viewdate</controlname></settings></field><field column="LastLogon" visible="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="UserStartingAliasPath" visible="false" dependsonanotherfield="false" /><field column="UserGUID" visible="false" dependsonanotherfield="false" /><field column="UserLastModified" visible="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><editTime>false</editTime></settings></field><field column="UserLastLogonInfo" visible="false" dependsonanotherfield="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserVisibility" visible="false" dependsonanotherfield="false" /><field column="UserIsDomain" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserHasAllowedCultures" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserSiteManagerDisabled" dependsonanotherfield="false" /><field column="UserSettingsID" visible="false" dependsonanotherfield="false" /><field column="UserNickName" visible="false" dependsonanotherfield="false" /><field column="UserPicture" visible="false" dependsonanotherfield="false" /><field column="UserSignature" visible="false" dependsonanotherfield="false" /><field column="UserURLReferrer" visible="false" dependsonanotherfield="false" /><field column="UserCampaign" visible="false" dependsonanotherfield="false" /><field column="UserMessagingNotificationEmail" visible="false" dependsonanotherfield="false" /><field column="UserCustomData" visible="false" dependsonanotherfield="false" /><field column="UserRegistrationInfo" visible="false" dependsonanotherfield="false" /><field column="UserPreferences" visible="false" dependsonanotherfield="false" /><field column="UserActivationDate" visible="false" dependsonanotherfield="false"><settings><timezonetype>inherit</timezonetype></settings></field><field column="UserActivatedByUserID" visible="false" dependsonanotherfield="false" /><field column="UserTimeZoneID" visible="false" dependsonanotherfield="false" /><field column="UserAvatarID" dependsonanotherfield="false"><settings><controlname>viewuseravatar</controlname></settings></field><field column="UserBadgeID" fieldcaption="Badge" dependsonanotherfield="false"><settings><controlname>viewbadgeimage</controlname></settings></field><field column="UserShowSplashScreen" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserActivityPoints" fieldcaption="Community points" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserForumPosts" fieldcaption="Forum posts" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserBlogComments" fieldcaption="Blog comments" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserGender" fieldcaption="Gender" dependsonanotherfield="false"><settings><controlname>viewusergender</controlname></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth" dependsonanotherfield="false" visibility=""><settings><controlname>viewdate</controlname><DisplayNow /><TimeZoneType /><EditTime /></settings></field><field column="UserMessageBoardPosts" fieldcaption="Message board posts" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserSettingsUserGUID" visible="false" dependsonanotherfield="false" /><field column="UserSettingsUserID" visible="false" dependsonanotherfield="false" /><field column="WindowsLiveID" dependsonanotherfield="false" /><field column="UserBlogPosts" fieldcaption="Blog posts" dependsonanotherfield="false"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserWaitingForApproval" visible="false" defaultvalue="false" dependsonanotherfield="false" /><field column="UserDialogsConfiguration" visible="false" dependsonanotherfield="false" /><field column="UserDescription" visible="false" dependsonanotherfield="false" /><field column="UserUsedWebParts" dependsonanotherfield="false" /><field column="UserUsedWidgets" dependsonanotherfield="false" /><field column="UserFacebookID" dependsonanotherfield="false" /><field column="UserAuthenticationGUID" dependsonanotherfield="false" /><field column="UserSkype" fieldcaption="Skype account" visible="true" dependsonanotherfield="false" visibility=""><settings><controlname>viewsecuretext</controlname></settings></field><field column="UserIM" fieldcaption="Instant messenger" visible="true" dependsonanotherfield="false" visibility=""><settings><controlname>viewsecuretext</controlname></settings></field><field column="UserPhone" fieldcaption="Phone number" visible="true" dependsonanotherfield="false" visibility=""><settings><controlname>viewsecuretext</controlname></settings></field><field column="UserPosition" fieldcaption="Position" visible="true" dependsonanotherfield="false" visibility=""><settings><controlname>viewsecuretext</controlname></settings></field><field column="UserBounces" visible="false" dependsonanotherfield="false" /></form>', N'<table width="100%" cellpadding="2">
    <tbody>
        <tr>
            <td style="width: 140px;"><strong>$$label:UserPosition$$</strong></td>
            <td>$$input:UserPosition$$</td>
            <td style="width: 150px;"><strong>$$label:UserForumPosts$$</strong></td>
            <td>$$input:UserForumPosts$$</td>
            <td valign="top" rowspan="8">$$input:UserAvatarID$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserName$$</strong></td>
            <td>$$input:UserName$$</td>
            <td><strong>$$label:UserMessageBoardPosts$$</strong></td>
            <td>$$input:UserMessageBoardPosts$$</td>
        </tr>
        <tr>
            <td><strong>$$label:Email$$</strong></td>
            <td>$$input:Email$$</td>
            <td><strong>$$label:UserBlogPosts$$</strong></td>
            <td>$$input:UserBlogPosts$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserPhone$$</strong></td>
            <td>$$input:UserPhone$$</td>
            <td><strong>$$label:UserBlogComments$$</strong></td>
            <td>$$input:UserBlogComments$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserSkype$$</strong></td>
            <td>$$input:UserSkype$$</td>
            <td><strong>$$label:UserActivityPoints$$</strong></td>
            <td>$$input:UserActivityPoints$$</td>
        </tr>
        <tr>
            <td><strong>$$label:UserIM$$</strong></td>
            <td>$$input:UserIM$$</td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserGender$$</strong></td>
            <td>$$input:UserGender$$</td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserDateOfBirth$$</strong></td>
            <td>$$input:UserDateOfBirth$$</td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td><strong>$$label:UserCreated$$</strong></td>
            <td>$$input:UserCreated$$</td>
            <td colspan="2">&nbsp;</td>
        </tr>
    </tbody>
</table>', 'bbed0652-dd65-436b-89c9-634cce6a05af', '20110225 12:46:36', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (1024, N'Display profile (Corporate Site)', N'DisplayProfileCorporateSite', 59, N'<form><field column="UserID" visible="false" /><field column="UserName" fieldcaption="Name"><settings><controlname>viewsecuretext</controlname></settings></field><field column="FirstName" visible="false" /><field column="MiddleName" visible="false" /><field column="LastName" visible="false" /><field column="FullName" fieldcaption="Full name"><settings><controlname>viewsecuretext</controlname></settings></field><field column="Email" fieldcaption="E-mail"><settings><controlname>labelcontrol</controlname><FilterMode /><FilterEnabled /></settings></field><field column="UserPassword" visible="false" /><field column="PreferredCultureCode" visible="false" /><field column="PreferredUICultureCode" visible="false" /><field column="UserEnabled" visible="false" defaultvalue="false" /><field column="UserIsEditor" visible="false" defaultvalue="false" /><field column="UserIsGlobalAdministrator" visible="false" defaultvalue="false" /><field column="UserIsExternal" visible="false" defaultvalue="false" /><field column="UserPasswordFormat" visible="false" /><field column="UserCreated" fieldcaption="Created"><settings><controlname>viewdate</controlname></settings></field><field column="LastLogon" visible="false" /><field column="UserStartingAliasPath" visible="false" /><field column="UserGUID" visible="false" /><field column="UserLastModified" visible="false" /><field column="UserLastLogonInfo" visible="false" /><field column="UserIsHidden" visible="false" defaultvalue="false" /><field column="UserVisibility" visible="false" /><field column="UserIsDomain" defaultvalue="false" /><field column="UserSettingsID" visible="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserPicture" visible="false" /><field column="UserSignature" visible="false"><settings><FilterEnabled /></settings></field><field column="UserURLReferrer" visible="false" /><field column="UserCampaign" visible="false" /><field column="UserMessagingNotificationEmail" visible="false" /><field column="UserCustomData" visible="false"><settings><Dialogs_Web_Hide>False</Dialogs_Web_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><controlname>bbeditorcontrol</controlname><Dialogs_Content_Hide>False</Dialogs_Content_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="UserRegistrationInfo" visible="false"><settings><Dialogs_Web_Hide>False</Dialogs_Web_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><controlname>bbeditorcontrol</controlname><Dialogs_Content_Hide>False</Dialogs_Content_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="UserPreferences" visible="false"><settings><Dialogs_Web_Hide>False</Dialogs_Web_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><controlname>bbeditorcontrol</controlname><Dialogs_Content_Hide>False</Dialogs_Content_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="UserActivationDate" visible="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="UserActivatedByUserID" visible="false" /><field column="UserTimeZoneID" visible="false" /><field column="UserAvatarID"><settings><controlname>viewuseravatar</controlname></settings></field><field column="UserBadgeID"><settings><controlname>viewbadgeimage</controlname></settings></field><field column="UserShowSplashScreen" visible="false" defaultvalue="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserActivityPoints" fieldcaption="Community points"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserForumPosts" fieldcaption="Forum posts"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserBlogComments" fieldcaption="Blog comments"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserGender" fieldcaption="Gender"><settings><controlname>viewusergender</controlname></settings></field><field column="UserDateOfBirth" fieldcaption="Date of birth"><settings><controlname>viewdate</controlname><DisplayNow /><TimeZoneType /><EditTime /></settings></field><field column="UserMessageBoardPosts" fieldcaption="Message board posts"><settings><controlname>viewintegernumber</controlname></settings></field><field column="UserSettingsUserGUID" visible="false" /><field column="UserSettingsUserID" visible="false" /><field column="UserBlogPosts" fieldcaption="Blog posts"><settings><controlname>viewintegernumber</controlname></settings></field></form>', N'<table cellpadding="2" class="profile">
 <tbody>
  <tr>
   <td class="listBoxWithTeaser image" colspan="2" rowspan="8">
    <div class="teaser">
     $$input:UserAvatarID$$</div>
   </td>
   <td colspan="2">
    <div class="header">
     $$input:UserName$$</div>
   </td>
  </tr>
  <tr>
   <td>
    $$input:UserBadgeID$$</td>
   <td>
    &nbsp;</td>
  </tr>
  <tr>
   <td>
    <strong>$$label:FullName$$</strong></td>
   <td>
    $$input:FullName$$</td>
  </tr>
  <tr>
   <td>
    &nbsp;</td>
   <td>
    &nbsp;</td>
  </tr>
  <tr>
   <td>
    <strong>$$label:Email$$</strong></td>
   <td>
    $$input:Email$$</td>
  </tr>
  <tr>
   <td>
    <strong>$$label:UserCreated$$</strong></td>
   <td>
    $$input:UserCreated$$</td>
  </tr>
  <tr>
   <td>
    <strong>$$label:UserGender$$</strong></td>
   <td>
    $$input:UserGender$$</td>
  </tr>
  <tr>
   <td>
    <strong>$$label:UserDateOfBirth$$</strong></td>
   <td>
    $$input:UserDateOfBirth$$</td>
  </tr>
  <tr>
   <td colspan="2">
    &nbsp;</td>
   <td colspan="2">
    <table class="profileStats">
     <tbody>
      <tr>
       <td>
        $$label:UserForumPosts$$</td>
       <td>
        $$input:UserForumPosts$$</td>
      </tr>
      <tr>
       <td>
        $$label:UserMessageBoardPosts$$</td>
       <td>
        $$input:UserMessageBoardPosts$$</td>
      </tr>
      <tr>
       <td>
        $$label:UserBlogPosts$$</td>
       <td>
        $$input:UserBlogPosts$$</td>
      </tr>
      <tr>
       <td>
        $$label:UserBlogComments$$</td>
       <td>
        $$input:UserBlogComments$$</td>
      </tr>
      <tr>
       <td>
        $$label:UserActivityPoints$$</td>
       <td>
        $$input:UserActivityPoints$$</td>
      </tr>
     </tbody>
    </table>
   </td>
  </tr>
 </tbody>
</table>', '6067eff4-c8bf-4f0e-9775-3b60220dafcd', '20110720 21:51:47', 1768)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (1040, N'alt', N'alt', 2837, NULL, NULL, '3debb5bb-db3f-445c-9d75-a1961d2ecf67', '20110809 11:05:19', NULL)
INSERT INTO [CMS_AlternativeForm] ([FormID], [FormDisplayName], [FormName], [FormClassID], [FormDefinition], [FormLayout], [FormGUID], [FormLastModified], [FormCoupledClassID]) VALUES (1047, N'Scoring - attribute rule', N'ScoringAttributeRule', 2841, N'<form><field column="ContactID" visible="false" visibility="none" /><field column="ContactFirstName" fieldcaption="{$om.contact.firstname$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactMiddleName" fieldcaption="{$om.contact.middlename$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactLastName" fieldcaption="{$om.contact.lastname$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactSalutation" fieldcaption="{$om.contact.salutation$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactTitleBefore" fieldcaption="{$om.contact.titlebefore$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactTitleAfter" fieldcaption="{$om.contact.titleafter$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactJobTitle" fieldcaption="{$om.contact.jobtitle$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactAddress1" fieldcaption="{$om.contact.address1$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactAddress2" fieldcaption="{$om.contact.address2$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactCity" fieldcaption="{$general.city$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactZIP" fieldcaption="{$general.zip$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactStateID" fieldcaption="{$general.state$}" visibility="none"><settings><controlname>countryselector</controlname><ReturnType>2</ReturnType></settings></field><field column="ContactCountryID" fieldcaption="{$general.country$}" visibility="none"><settings><controlname>countryselector</controlname><ReturnType>1</ReturnType></settings></field><field column="ContactMobilePhone" fieldcaption="{$om.contact.mobilephone$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactHomePhone" fieldcaption="{$om.contact.homephone$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactBusinessPhone" fieldcaption="{$om.contact.businessphone$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactEmail" fieldcaption="{$general.email$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactWebSite" fieldcaption="{$om.contact.website$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactBirthday" fieldcaption="{$om.contact.birthday$}" visibility="none"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>datetimefilter</controlname><EditTime>False</EditTime></settings></field><field column="ContactGender" fieldcaption="{$om.contact.gender$}" visibility="none"><settings><DisplayType>0</DisplayType><controlname>genderselector</controlname></settings></field><field column="ContactStatusID" fieldcaption="{$om.contactstatus$}" visibility="none"><settings><controlname>contactstatusselector</controlname><DisplaySiteOrGlobal>True</DisplaySiteOrGlobal><AllowAllItem>False</AllowAllItem></settings></field><field column="ContactNotes" fieldcaption="{$om.contact.notes$}" visibility="none"><settings><controlname>textfilter</controlname></settings></field><field column="ContactOwnerUserID" fieldcaption="{$om.contact.owner$}" visibility="none"><settings><controlname>userselector</controlname></settings></field><field column="ContactMonitored" fieldcaption="{$om.contact.tracking$}" defaultvalue="false" visibility="none"><settings><controlname>booleanfilter</controlname></settings></field><field column="ContactMergedWithContactID" fieldcaption="Merged into contact" visible="false" visibility="none"><settings><controlname>contactselector</controlname></settings></field><field column="ContactIsAnonymous" fieldcaption="{$om.contact.isanonymous$}" defaultvalue="false"><settings><controlname>booleanfilter</controlname></settings></field><field column="ContactSiteID" fieldcaption="Site" visible="false" visibility="none"><settings><controlname>selectsite</controlname></settings></field><field column="ContactGUID" visible="false" visibility="none" /><field column="ContactLastModified" fieldcaption="{$general.lastmodified$}" visibility="none"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>datetimefilter</controlname><EditTime>True</EditTime></settings></field><field column="ContactCreated" fieldcaption="{$om.contact.created$}" visibility="none"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>datetimefilter</controlname><EditTime>True</EditTime></settings></field><field column="ContactMergedWhen" fieldcaption="Merged when" visible="false" visibility="none"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>datetimefilter</controlname><EditTime>True</EditTime></settings></field><field column="ContactGlobalContactID" visible="false" visibility="none" /><field column="ContactBounces" fieldcaption="{$unigrid.newsletter_issue.columns.issuebounces$}" visibility="none"><settings><controlname>numberfilter</controlname></settings></field><field column="ContactLastLogon" fieldcaption="{$general.lastlogon$}" visibility="none"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>datetimefilter</controlname><EditTime>True</EditTime></settings></field></form>', NULL, 'a1aba880-d441-4b29-a0fa-fcb33352892d', '20110907 12:04:01', NULL)
SET IDENTITY_INSERT [CMS_AlternativeForm] OFF
