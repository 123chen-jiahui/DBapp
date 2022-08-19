<template>
  <v-container
    id="work-view"
    fluid
    tag="section"
  >
    <v-row>
      <v-col cols="6">
        <v-card
          max-width="344"
        >
<!--          <template #heading>-->
<!--            <div class="pa-8 white&#45;&#45;text">-->
<!--              <div class="text-h4 font-weight-light">-->
<!--              ABC-->
<!--              </div>-->
<!--              <div class="text-caption">-->
<!--                病人信息-->
<!--              </div>-->
<!--            </div>-->
<!--          </template>-->
          <v-card-text>
            <div  class="text-h4 text--primary">今天{{currentIndex+1}}号:{{ patientInfo.name }}</div>
            <p>
              性别:{{patientInfo.gender}}
            </p>
            <p>              年龄:{{patientInfo.age}}
            </p>
            <div class="text--primary">
              id:{{patientInfo.id}}<br>

            </div>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="6">
        <v-btn @click="nextOne">下一个</v-btn>
      </v-col>



    </v-row>
    <v-row>
      <v-col cols="3"><v-btn>编辑医疗项目</v-btn></v-col>
      <v-col cols="3"><v-btn>编辑诊断结果</v-btn></v-col>
    </v-row>
    <v-row>
      <v-col cols="3">  <v-dialog
        v-model="dialogEditShoppingCart"
        width="500"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn @click="dialogEditShoppingCart=!dialogEditShoppingCart">编辑处方</v-btn>
        </template>
        <v-card>
          <v-card-title class="text-h5 grey lighten-2">
            编辑处方
          </v-card-title>
          <v-card-title class="text-h6 white lighten-2">
            当前处方
          </v-card-title>

          <v-card-text v-for="item in shoppingCartInfo.shoppingCartItems">
            {{item.medicineId}}:{{item['medicine'].name}}<br>
            价格:{{item['medicine']['price']}}
          </v-card-text>
          <v-card-text>
            <v-autocomplete
              outlined
              v-model="keyword"
              :items="searchMedicineList"
              :search-input.sync="search"
              prepend-icon="mdi-database-search"
              hide-no-data
              item-text="name"
              item-value="name"
            ></v-autocomplete>

          </v-card-text>
          <v-card-text v-for="item in searchMedicineList">

          </v-card-text>
          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="primary"
              text
              @click="addToShoppingCart"
            >
              加入
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn
              color="primary"
              text
              @click="dialogEditShoppingCart = false"
            >
              返回
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog></v-col>
      <v-col cols="3">
        <v-dialog
          v-model="dialog"
          width="500"
        >
          <template v-slot:activator="{ on, attrs }">
        <v-btn @click="dialog=!dialog">安排手术</v-btn>
          </template>
          <v-card>
            <v-card-title class="text-h5 grey lighten-2">
             手术安排
            </v-card-title>

            <v-card-text>
              住院安排
              <v-text-field
                label="Regular"
                v-model="arrangement"
              ></v-text-field>
            </v-card-text>
            <v-card-text>
              手术时间
              <v-date-picker
                v-model="date"
                :allowed-dates="allowedDates"
                class="mt-4"
                min="2022-06-15"
                max="2030-03-20"
              ></v-date-picker>
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="primary"
                text
                @click="dialog = false"
              >
               保存
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

      </v-col>
    </v-row>

  </v-container>
</template>
/**
 * 组件职责:
 * Created by Zony on 9/8/2022
 * @author Zony Zhao
 */
<script>

import axios from 'axios';
export default {
  name: 'Work',
  // props:['token'],
  components: {},
  mounted() {
    const day=new Date().getDay()
    axios({
      method: 'get',
      url: `http://localhost:8080/waitline/${day}`,
      headers: {
        'Authorization': `bearer ${this.token}`
      }
    }).then(r=>{
      console.log( r );
      this.patientList=r.data
      return this .getCurrentInfo()
    }).then(r=>{
      return this .getShoppingCart()
    })

    // console.log( this.user );
    // console.log( 'about');
  },
  computed: {


  },
  data: () => ({
    //doctor token
    token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMDAwMDIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRG9jdG9yIiwibmJmIjoxNjYwNzAwNTM4LCJleHAiOjE2NjA3ODY5MzgsImlzcyI6IlRvbmdqaUhvc3BpdGFsLmNvbSIsImF1ZCI6IlRvbmdqaUhvc3BpdGFsLmNvbSJ9.mtcixukPbV2SmaOIr26hJcCTNShKvJtee26_qTE1PVw",
    patientList:[],
    currentIndex:0,
    date: '2022-08-15',
    dialog: false,
    dialogEditShoppingCart: false,
    arrangement:'',
    patientInfo:{
      id:0,
      name:"-",
      age:0,
      gender:"-"
    },
    shoppingCartInfo:{
      id:'',
      patientId:0,
      shoppingCartItems:[]
    },
    searchMedicineList:[],
    keyword:"",
    search:null,
    medicineId:'Z20040063'
  }),
  watch: {
    search (val,old) {
      val && val !== old && this.searchMedicine(val)
    },
  },
  methods: {
    getShoppingCart:function (){
      axios({
        method: 'get',
        url: `http://localhost:8080/api/shoppingCart/${this.patientList[this.currentIndex]['patientId']}`,
        headers: {
          'Authorization': `bearer ${this.token}`,
          'Access-Control-Allow-Origin': '*',
        }
      }).then(r=>{
        console.log( r );
        this.shoppingCartInfo=r.data

      })
    },
    searchMedicine:function (val) {
      axios({
        method: 'get',
        url: `http://localhost:8080/medicine?keyWord=${val}`,
        headers: {
          'Authorization': `bearer ${this.token}`,
          'Access-Control-Allow-Origin': '*',
        },

      }).then(r=>{
        console.log( r );
        this.searchMedicineList=r.data
      })
    },
    addToShoppingCart:function (){
      axios({
        method: 'post',
        url: `http://localhost:8080/api/shoppingCart/items/${this.patientList[this.currentIndex]['patientId']}`,
        headers: {
          'Authorization': `bearer ${this.token}`,
          'Access-Control-Allow-Origin': '*',
        },
        data:{
          "Id":this.searchMedicineList.find(m=>m.name==this.search).id.toString()
        }
      }).then(r=>{
        console.log( r );
      this.shoppingCartInfo=r.data
      })
    },
    nextOne:function (){
      if (this.currentIndex>=this.patientList.length-1){
        this.currentIndex=0
      }else{
        this.currentIndex++
      }
      this.getCurrentInfo()
    },
    getCurrentInfo:function (){
      axios({
        method: 'get',
        url: `http://localhost:8080/patients/${this.patientList[this.currentIndex]['patientId']}`,
        headers: {
          'Authorization': `bearer ${this.token}`,
          'Access-Control-Allow-Origin': '*',
        }
      }).then(r=>{
        console.log( r );
        this.patientInfo=r.data
      })
},
    allowedDates: val => parseInt(val.split('-')[2], 10) % 2 === 0,
  }
};
</script>

<style lang="scss" scoped>

</style>