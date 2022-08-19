<template>
  <v-container
    id="work-view"
    fluid
    tag="section"
  >
    <v-row>
      <v-card>
        <v-card-text v-for="item in items">
          {{ item.medicineId }}:{{ item['medicine'].name }}<br>
          价格:{{ item['medicine']['price'] }}
        </v-card-text>
        <v-card-text>
          <v-text-field
            v-model="orderId"
            label="取药单号"
            outlined
          />
        </v-card-text>

        <v-divider />

        <v-card-actions>
          <v-spacer />
          <v-btn
            color="primary"
            text
            @click="get"
          >
            查询
          </v-btn>
          <v-divider />
          <v-spacer />

          <v-btn
            color="primary"
            text
            @click="deleteMedicine"
          >
            取药完成
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-row>
  </v-container>
</template>
/**
 * 组件职责:
 * Created by Zony on 9/8/2022
 * @author Zony Zhao
 */
<script>

  import axios from 'axios'
  export default {
    name: 'Work',
    // props:['token'],
    components: {},
    data: () => ({
      // doctor token
      token: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMDAwMDAwIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTWVkaWNpbmVUb2tlbiIsIm5iZiI6MTY2MDczMDAzNywiZXhwIjoxNjYzNDA4NDM3LCJpc3MiOiJUb25namlIb3NwaXRhbC5jb20iLCJhdWQiOiJUb25namlIb3NwaXRhbC5jb20ifQ.vLKJ2_JDKi3yRe323J9Vg3XGhzqCJFKgwvOYGNnsrgw',
      items: {},
      orderId: 'dbc56c71-6d29-4fe0-9797-85243a91be40',
    }),
    computed: {

    },
    mounted () {
      const day = new Date().getDay()
      axios({
        method: 'get',
        url: `http://localhost:8080/waitline/${day}`,
        headers: {
          Authorization: `bearer ${this.token}`,
        },
      }).then(r => {
        console.log(r)
        this.patientList = r.data
        return this.getCurrentInfo()
      }).then(r => {
        return this.getShoppingCart()
      })
    // console.log( this.user );
    // console.log( 'about');
    },
    methods: {
      get: function () {
        axios({
          method: 'get',
          url: `http://localhost:8080/api/orders/${this.orderId}`,
          headers: {
            Authorization: `bearer ${this.token}`,
            'Access-Control-Allow-Origin': '*',
          },
        }).then(r => {
          console.log(r.data)
          this.items = r.data.orderItems
        })
      },
      deleteMedicine: function () {
        axios({
          method: 'get',
          url: `http://localhost:8080/api/orders/${this.orderId}`,
          headers: {
            Authorization: `bearer ${this.token}`,
            'Access-Control-Allow-Origin': '*',
          },
        }).then(r => {
          console.log(r.data)
          this.items = r.data.orderItems
        })
      },
      allowedDates: val => parseInt(val.split('-')[2], 10) % 2 === 0,
    },
  }
</script>

<style lang="scss" scoped>

</style>
