import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Cinemahall } from 'src/app/models/cinemahall/cinemahall';
import { Movie } from 'src/app/models/movie/movie';
import { Show } from 'src/app/models/show/show';
import { GenericService } from 'src/app/services/generic.services';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})
export class ShowComponent {
  mshowForm!: FormGroup;
  mshowList: Show[] =[];
  hallList: Cinemahall[] = [];
  movieList: Movie[] = [];
  currentshow: Show | null = null;
  isEditMode: boolean = false;
  showForm: boolean = false;
  showList: boolean = true;

  initForm(show?: Show): void {
    this.mshowForm = new FormGroup({
      showID: new FormControl(show ? show.showID : ''),
      hallID: new FormControl(show ? show.hallID : '', Validators.required),
      movieID: new FormControl(show ? show.movieID : '', Validators.required),
      price: new FormControl(show ? show.price : '', Validators.required),
      showDateTime: new FormControl(show ? show.showdatetime : '', Validators.required),
    });
  }
  
  constructor(private showService: GenericService<Show>, private hallService: GenericService<Cinemahall>, private movieService: GenericService<Movie>) {
    this.initForm();
  }
    ngOnInit() {
      this.fetchShow();
      this.initForm();
    }
  
    fetchShow() {
      this.showService.getAll("show").subscribe(data => {
        this.mshowList = data;
      });
  
      this.hallService.getAll("cinemhall").subscribe(data => {
        this.hallList = data;  
      });

      this.movieService.getAll("movie").subscribe(data => {
        this.movieList = data;  
      });
    }
  
    editShow(show: Show): void {
      this.currentshow = show;
      console.log("CurrentShow: ", this.currentshow);
      this.isEditMode = true;
      this.showForm = true;
      this.showList = false;
      this.initForm(show);
    }
  
    resetForm(): void {
      this.mshowForm.reset();
      this.showForm = false;
      this.showList = true;
      this.isEditMode = false;
      this.currentshow = null;
    }
  
    toggleSave(): void {
      this.isEditMode = false;
      this.currentshow = null;
      this.initForm();
      this.showForm = !this.showForm;
      this.showList = !this.showForm;
    }
  
    public save(): void {
      if (this.mshowForm.valid) {
        const formdata = this.mshowForm.value;
        console.log("Formdata: ", formdata);
        
        const showid = this.isEditMode ? formdata.showID : 0;
  
        const showData = {
          showID: showid,
          hallID: parseInt(formdata.hallID, 10),
          movieID: parseInt(formdata.movieID, 10),
          price: parseInt(formdata.price, 10),
          showdatetime: formdata.showDateTime
        };

        if (this.isEditMode == true && showData.showID) {  
          this.showService.update('show', showData, showData.showID).subscribe({
            next: (response) => {
              console.log('show updated:', response);
              this.resetForm();
              this.fetchShow();
            },
            error: (error) => {
              console.error('Failed to update show:', error);
              alert(`Failed to update show: ${error.error.title}`);
            }
          });
        }
           else
           {
            console.log("show Create: ", showData);
            
              this.showService.create('show', showData).subscribe({
                next: (response) => {
                  console.log('show saved:', response);
                  this.mshowList.push(response);
                  this.mshowForm.reset();
                  this.showForm = false;
                  this.showList = true;
                },
                error: (error) => {
                  console.error('Failed to create show:', error);
                  alert(`Failed to create show: ${error.error.title}`);
                }
              });
      }
    }
   }
    
    deleteArea(show: Show) {
      if (show && show.showID !== undefined) {
          this.showService.delete('show', show.showID).subscribe(() => {
              this.fetchShow();
          });
      }
    }
  }

