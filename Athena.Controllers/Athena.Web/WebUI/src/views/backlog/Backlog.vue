<template>
  <div>
    <h1>Backlog</h1>

    <table>
      <thead>
        <tr>
          <th>Id</th>
          <th>Title</th>
          <th>Assigned To</th>
          <th>State</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" v-bind:key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.title }}</td>
          <td>{{ item.assignedTo }}</td>
          <td>{{ item.state }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import API from "@/api";

export default {
  name: "Backlog",

  data() {
    return {
      items: [],
    };
  },
  
  async created() {
    const response = await API.get("/backlog/teams/TeamA");

    this.items = response.data.workItems;
  },
};
</script>
