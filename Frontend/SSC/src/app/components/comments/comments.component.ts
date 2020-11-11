import { Component, OnInit } from '@angular/core';
import { FreeapiService } from '../../services/freeapi.service';
import { CommentInfomation } from '../../shared/models/interfaces/Comments.interface';
import { PostInfomation } from '../../shared/models/interfaces/Posts.interface';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { emailValidator } from '../../shared/CustomValidators';
import { UsersInfomation } from '../../shared/models/interfaces/Users.interface';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {
  userFormGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private _freeapiService: FreeapiService,
    private _UsersService: UsersService,
  ) {
    this.createForm(); 
  }

  
  public postForm = new FormGroup({
    title: new FormControl('', Validators.required),
    body : new FormControl(''),
  })

  listComments: CommentInfomation[];
  listPosts: PostInfomation[];
  listUsers: UsersInfomation[];

  ngOnInit(): void {
    this._freeapiService.getcommnets()
      .subscribe(
        data => {
          this.listComments = data;
        }
    );

    this._freeapiService.getcommnetsbyparameter(2)
      .subscribe(
        data => {
          this.listPosts = data;
        }
    );

    this._UsersService.getUser()
      .subscribe(
        data => {
          this.listUsers = data;
        }
      );
  }

  onSubmit(data) {
    this.http.post("http://jsonplaceholder.typicode.com/posts", data)
      .subscribe(
        (result) => {
          console.log(result);
        }
      );
  }

  createForm() {
    this.userFormGroup = this.formBuilder.group({
      name: ['Linh', [Validators.required, Validators.minLength(4)]],
      username: ['', Validators.required],
      email: ['', [Validators.required, emailValidator()]],
      address: this.formBuilder.group({
        street: ['', Validators.required],
      })
    });
  }

  getIdUser(user) {
    const idUser = user.id;
    console.log(idUser);
    this._freeapiService.getcommnetsbyparameter(idUser)
      .subscribe(
        data => {
          this.listPosts = data;
        }
      );
  }
}
