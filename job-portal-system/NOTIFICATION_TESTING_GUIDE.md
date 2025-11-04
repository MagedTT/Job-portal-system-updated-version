# ?? Notification System Testing Guide

## ? Notification Integration Complete!

The notification system is now fully integrated with your application. Here's how to test it:

---

## ?? Test Scenarios

### 1. **Test Admin Notifications**

#### Test 1.1: New User Registration
**Steps:**
1. Open browser in **incognito mode**
2. Navigate to `/Account/RegisterJobSeeker`
3. Register a new job seeker (e.g., `testjs@test.com`)
4. In another browser/tab, login as **Admin** (`admin@jobverse.com`)
5. Check notification bell - should show **1 unread notification**
6. Click bell - should see "New Job Seeker Registered"

**Expected:** Admin receives notification when new job seeker registers ?

#### Test 1.2: New Job Post
**Steps:**
1. Login as **Employer** (`employer1@jobverse.com`)
2. Navigate to `/Job/Create`
3. Create a new job
4. Login as **Admin** in another tab
5. Check notification bell

**Expected:** Admin receives notification about new job post ?

#### Test 1.3: New Job Application
**Steps:**
1. Login as **Job Seeker** (`jobseeker1@jobverse.com`)
2. Browse jobs and apply to one
3. Login as **Admin** in another tab
4. Check notification bell

**Expected:** Admin receives notification about new application ?

---

### 2. **Test Employer Notifications**

#### Test 2.1: Job Application Received
**Steps:**
1. Login as **Job Seeker** (`jobseeker1@jobverse.com`)
2. Navigate to `/Job/Index`
3. Find a job from employer1@jobverse.com
4. Click "Apply Now" and submit application
5. **In another browser/tab**, login as **Employer** (`employer1@jobverse.com`)
6. Click the notification bell

**Expected:** Employer receives real-time notification: "New Application Received" ?

**Note:** If both users are logged in simultaneously, the notification should appear **instantly** via SignalR!

---

### 3. **Test Job Seeker Notifications**

#### Test 3.1: New Job Posted
**Steps:**
1. Login as **Job Seeker** (`jobseeker1@jobverse.com`)
2. Keep the browser tab open
3. **In another browser/tab**, login as **Employer** (`employer1@jobverse.com`)
4. Create a new job
5. Switch back to job seeker tab
6. **Watch the notification bell** - it should update in real-time!

**Expected:** Job seeker receives real-time notification about new job ?

#### Test 3.2: Application Accepted
**Steps:**
1. Login as **Employer** (`employer1@jobverse.com`)
2. Navigate to `/Employer/Applicants`
3. Find a pending application
4. Click **"Accept"** button
5. **In another browser/tab**, login as the **Job Seeker** who applied
6. Check notification bell

**Expected:** Job seeker receives notification: "Application Accepted! ??" ?

#### Test 3.3: Application Rejected
**Steps:**
1. Login as **Employer**
2. Navigate to `/Employer/Applicants`
3. Find a pending application
4. Click **"Reject"** button
5. Switch to job seeker account
6. Check notification bell

**Expected:** Job seeker receives notification: "Application Status Updated" ?

---

## ?? Debugging Tips

### Check Browser Console
Open Developer Tools (F12) and check Console tab for:
```
SignalR Connected to Notification Hub
```

### Check Network Tab
1. Open Developer Tools ? Network tab
2. Filter by "notificationHub"
3. Should see WebSocket connection established

### Check Database
Run this SQL query to see all notifications:
```sql
SELECT * FROM Notifications ORDER BY CreatedAt DESC
```

### Common Issues & Solutions

#### Issue: No notifications appearing
**Solution:**
1. Check browser console for errors
2. Verify SignalR connection is established
3. Check database - are notifications being created?
4. Clear browser cache and reload

#### Issue: Notifications not real-time
**Solution:**
1. Check browser console for "SignalR Connected" message
2. Verify both users are logged in
3. Try refreshing the page
4. Check firewall/antivirus isn't blocking WebSockets

#### Issue: Notification bell doesn't show count
**Solution:**
1. Check browser console for JavaScript errors
2. Verify `/Notification/GetUnreadCount` returns data
3. Inspect notification badge element - should show when count > 0

---

## ?? Expected Results Summary

After running all tests, you should have:

| User Role | Expected Notifications |
|-----------|----------------------|
| **Admin** | 3+ notifications (new users, new jobs, new applications) |
| **Employer** | 1+ notification (job applications received) |
| **Job Seeker** | 2+ notifications (new jobs, application status changes) |

---

## ?? Real-Time Test

**Best way to see SignalR in action:**

1. Open **two browser windows side-by-side**
2. **Left window:** Login as Job Seeker
3. **Right window:** Login as Employer
4. In Employer window: Go to `/Employer/Applicants`
5. Accept or reject an application
6. **Watch the left window** (Job Seeker) - notification bell should update **instantly**!

---

## ? Verification Checklist

- [ ] Admin receives notification when new user registers
- [ ] Admin receives notification when new job is created
- [ ] Admin receives notification when application is submitted
- [ ] Employer receives notification when someone applies to their job
- [ ] Job seeker receives notification when new job is posted
- [ ] Job seeker receives notification when application is accepted
- [ ] Job seeker receives notification when application is rejected
- [ ] Notifications appear in real-time (via SignalR)
- [ ] Notification badge shows correct unread count
- [ ] Clicking notification marks it as read
- [ ] "Mark all as read" button works
- [ ] Notifications persist in database
- [ ] Clicking notification navigates to correct page

---

## ?? Testing Tips

1. **Use multiple browsers** (Chrome, Edge, Firefox) to simulate different users
2. **Use incognito/private windows** to avoid login conflicts
3. **Keep Developer Tools open** to monitor SignalR connection
4. **Test with real-time updates** - keep both users logged in simultaneously
5. **Check database** after each action to verify notifications are created

---

## ?? Troubleshooting

If something doesn't work:

1. **Check browser console** for errors
2. **Verify SignalR connection** - should see "Connected" message
3. **Check database** - run `SELECT * FROM Notifications`
4. **Verify user is logged in** - notifications only work for authenticated users
5. **Clear cache** and try again
6. **Check Program.cs** - ensure SignalR is registered and hub is mapped

---

## ?? Success Indicators

You'll know it's working when:

- ? Notification bell updates **instantly** when action happens
- ? Badge shows correct unread count
- ? Dropdown shows recent notifications with icons
- ? Notifications persist after page refresh
- ? "View all notifications" page shows full history
- ? Clicking notification navigates to relevant page

---

**Happy Testing! ??**

If all tests pass, your notification system is fully functional and ready for production!
