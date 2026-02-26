<script lang="ts">
    import '$lib/api-client';
    import {  getApiMembers, postApiMembers} from '$lib/api/sdk.gen';
    import type { Member } from '$lib/api/types.gen';

    let members = $state<Member[]>([]);
    let newName = $state('');
    let newEmail = $state('');

    async function loadMembers() {
        const { data } = await getApiMembers();
        if (data) members = data;
    }

    async function addMember() {
        await postApiMembers({
            body: { name: newName, email: newEmail }
        });
        newName = ''; newEmail = '';
        await loadMembers(); 
    }

    $effect(() => { loadMembers(); });
</script>

<h1>Campus Members</h1>

<div class="form">
    <input bind:value={newName} placeholder="Name" />
    <input bind:value={newEmail} placeholder="Email" />
    <button onclick={addMember}>Add Member</button>
</div>

<ul>
    {#each members as member}
        <li>{member.name} ({member.email})</li>
    {/each}
</ul>