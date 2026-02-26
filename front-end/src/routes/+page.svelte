<script>
  import { fade } from 'svelte/transition';
  import { fetchMembers, fetchBooks, fetchLaptops } from '$lib/api.js';
  import ItemForm from '$lib/components/ItemForm.svelte';
  import AssetsTable from '$lib/components/AssetsTable.svelte';
  import MembersSection from '$lib/components/MembersSection.svelte';

  let members = [];
  let books = [];
  let laptops = [];
  let message = "";

  // Fetch all data on mount
  import { onMount } from 'svelte';
  onMount(async () => {
    loadData();
  });

  async function loadData() {
    try {
      [members, books, laptops] = await Promise.all([
        fetchMembers(),
        fetchBooks(),
        fetchLaptops()
      ]);
    } catch (error) {
      console.error("[v0] Failed to load data", error);
    }
  }

  function handleMessage(msg) {
    message = msg;
    loadData();
    setTimeout(() => {
      message = "";
    }, 3000);
  }
</script>

<main class="min-h-screen bg-background p-6">
  <div class="max-w-7xl mx-auto space-y-6">
    <div class="text-center mb-8">
      <h1 class="text-4xl font-bold text-foreground mb-2">Library Asset Manager</h1>
      <p class="text-muted-foreground">Manage your books, laptops, and members</p>
    </div>

    {#if message}
      <div 
        class="bg-secondary border border-border rounded-lg p-4 text-foreground"
        transition:fade
      >
        {message}
      </div>
    {/if}

    <ItemForm onSuccess={handleMessage} />
    
    <AssetsTable 
      books={books} 
      laptops={laptops} 
      members={members}
      onUpdate={handleMessage}
    />
    
    <MembersSection 
      members={members}
      onUpdate={handleMessage}
    />
  </div>
</main>
