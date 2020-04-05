﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.ViewModels;
using cRegis.Mobile.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private IStudentService _studentService;
        private IModifyService _modifyService;

        public ProfilePage()
        {
            InitializeComponent();
            _studentService = new StudentService((string)Application.Current.Properties["jwt"]);
            _modifyService = new ModifyService((string)Application.Current.Properties["jwt"]);
            testInit();
        }

        async void testInit()
        {
            
            //calling api to get student's detail infomation
            Student curStudent =  await _studentService.getStudentAsync();
            string crehrs = await _studentService.getStudentCreditAsync();
            List<Enrolled> listE = await _studentService.getStudentEnrolledListAsync();
            List<EnrolledViewModel> listEnroll = new List<EnrolledViewModel>();

            foreach (Enrolled e in listE)
            {
                int tempI = e.courseId;
                Course tempC = await _studentService.getCourseAsync(tempI);
                listEnroll.Add(new EnrolledViewModel(tempC, e));
            }

            //List<Course> listC = await _studentService.getStudentCourseListAsync();
            //int crehrs = 0;
            //List<Course> listC = new List<Course>();
            StudentViewModel test = new StudentViewModel(curStudent, crehrs, listEnroll);
            BindingContext = test;
        }

        public async void ViewCourseDetail(object sender, EventArgs e)
        {
            //var chosenCourse = (Course)registeredCourseList.SelectedItem;

            var menuItem = sender as Button;
            var chosenModel = menuItem.CommandParameter as EnrolledViewModel;

            await Navigation.PushAsync(new CourseDetailPage(chosenModel.cour));
        }

        public async void DropCourse(object sender, EventArgs e)
        {
            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as EnrolledViewModel;

            string result = await _modifyService.dropCourseAsync(chosenCourse.eid);
            await DisplayAlert("Drop Course", "Drop Successful", "Okay");
            Navigation.InsertPageBefore(new ProfilePage(), this); 
            Navigation.PopAsync();
        }
    }
}